using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.IO;
using System.Reflection;
using EducationalManagementSystem.Client.Models;
using EducationalManagementSystem.Client.Models.UserModels;
using System.Collections.ObjectModel;
using EducationalManagementSystem.Client.Services.Exceptions;

namespace EducationalManagementSystem.Client.Services
{
    public interface IDataService
    {
        object GetValue(ObjectWithID obj, string propertyName);
        void SetValue(ObjectWithID obj, string propertyName, object value);
        ObjectWithID NewObject(Type type);
        object GetList(ObjectWithID obj, string propertyName);
        object GetDictionary(ObjectWithID obj, string propertyName);
    }

    public class DataServiceFactory
    {
        public static IDataService DataService { get; } = new DatabaseDataService();
    }

    public class DatabaseDataService : IDataService
    {
        private static MySqlConnection _Connection;
        public static MySqlConnection Connection
        {
            get
            {
                if (_Connection == null)
                {
                    // TODO: 加密连接字符串
                    _Connection = new MySqlConnection(@"server=cdb-7iseargu.bj.tencentcdb.com;port=10234;database=EducationalManagementSystem;Uid=Clubie;password=Clubie123456;");
                    _Connection.Open();
                }
                return _Connection;
            }
        }

        private static readonly Dictionary<Type, MySqlDbType> _TypeToDbType = new Dictionary<Type, MySqlDbType>()
        {
            { typeof(uint?), MySqlDbType.UInt32 },
            { typeof(double?), MySqlDbType.Double },
            { typeof(string), MySqlDbType.Text },
            { typeof(DateTime?), MySqlDbType.DateTime },
        };

        public object GetValue(ObjectWithID obj, string propertyName)
        {
            var property = obj.GetType().GetProperty(propertyName);
            var declaringType = property.DeclaringType;
            var propertyType = property.PropertyType;
            using (var cmd = Connection.CreateCommand())
            {
                cmd.CommandText = $"SELECT {propertyName} FROM {declaringType.Name} WHERE ID = {obj.ID}";
                using (var reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    // ObjectWithID子类
                    if (propertyType.IsSubclassOf(typeof(ObjectWithID)))
                    {
                        var id = (uint)reader[0];
                        return ObjectWithID.GetByID(id, propertyType.GUID);
                    }
                    // Nullable<Enum>
                    if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>) && propertyType.GenericTypeArguments[0].IsEnum)
                        return Enum.Parse(propertyType.GenericTypeArguments[0], (string)reader[0]);
                    // 一般类型
                    return reader[0];
                }
            }
        }

        public void SetValue(ObjectWithID obj, string propertyName, object value)
        {
            var property = obj.GetType().GetProperty(propertyName);
            var declaringType = property.DeclaringType;
            var propertyType = property.PropertyType;
            using (var cmd = Connection.CreateCommand())
            {
                cmd.CommandText = $"UPDATE {declaringType.Name} SET {property.Name} = @Value WHERE ID = {obj.ID}";
                // ObjectWithID子类
                if (value is ObjectWithID data)
                {
                    cmd.Parameters.Add("@Value", MySqlDbType.UInt32);
                    cmd.Parameters["@Value"].Value = data.ID;
                }
                // Nullable<Enum>
                else if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>) && propertyType.GenericTypeArguments[0].IsEnum)
                {
                    cmd.Parameters.Add("@Value", MySqlDbType.Text);
                    cmd.Parameters["@Value"].Value = value.ToString();
                }
                // 一般类型
                else
                {
                    cmd.Parameters.Add("@Value", _TypeToDbType[property.PropertyType]);
                    cmd.Parameters["@Value"].Value = value;
                }
                cmd.ExecuteNonQuery();
            }
        }

        public ObjectWithID NewObject(Type type)
        {
            var baseType = type;
            while (baseType.BaseType != typeof(ObjectWithID))
                baseType = baseType.BaseType;
            using (var cmd = Connection.CreateCommand())
            {
                cmd.CommandText = $"INSERT INTO {baseType.Name} (Type) VALUES (@Type)";
                cmd.Parameters.Add("@Type", MySqlDbType.Binary);
                cmd.Parameters["@Type"].Value = type.GUID.ToByteArray();
                cmd.ExecuteNonQuery();
            }
            uint id;
            using (var cmd = Connection.CreateCommand())
            {
                cmd.CommandText = $"SELECT LAST_INSERT_ID()";
                using (var reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    id = (uint)(ulong)reader[0];
                }
            }
            var tempType = type;
            while (tempType != baseType)
            {
                using (var cmd = Connection.CreateCommand())
                {
                    cmd.CommandText = $"INSERT INTO {tempType.Name} (ID) VALUES (@ID)";
                    cmd.Parameters.Add("@ID", MySqlDbType.UInt32);
                    cmd.Parameters["@ID"].Value = id;
                    cmd.ExecuteNonQuery();
                }
                tempType = tempType.BaseType;
            }
            return ObjectWithID.GetByID(id, type.GUID);
        }

        public object GetList(ObjectWithID obj, string propertyName)
        {
            var objType = obj.GetType();
            var property = objType.GetProperty(propertyName);
            var propertyType = property.PropertyType;
            var relatedType = propertyType.GenericTypeArguments[0];
            foreach (var relatedProperty in relatedType.GetProperties())
            {
                var relatedPropertyType = relatedProperty.PropertyType;
                // 多对1
                if (relatedPropertyType == objType)
                    using (var cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = $"SELECT ID FROM {relatedType.Name} WHERE {relatedProperty.Name} = {obj.ID}";
                        using (var reader = cmd.ExecuteReader())
                        {
                            var result = propertyType.GetConstructor(Type.EmptyTypes).Invoke(null);
                            var addFunc = propertyType.GetMethod("Add");
                            while (reader.Read())
                            {
                                var id = (uint)reader[0];
                                var data = ObjectWithID.GetByID(id, relatedType.GUID);
                                addFunc.Invoke(result, new object[] { data });
                            }
                            return result;
                        }
                    }
                // 多对多
                if (relatedPropertyType.IsGenericType && relatedPropertyType.GetGenericTypeDefinition() == typeof(ObservableCollection<>) && relatedPropertyType.GenericTypeArguments[0] == objType)
                    using (var cmd = Connection.CreateCommand())
                    {
                        var first = objType.Name;
                        var second = relatedType.Name;
                        if (string.Compare(first, second) > 0)
                        {
                            var temp = first;
                            first = second;
                            second = temp;
                        }
                        cmd.CommandText = $"SELECT {relatedType.Name}ID FROM {first}_{second} WHERE {objType.Name} = {obj.ID}";
                        using (var reader = cmd.ExecuteReader())
                        {
                            var result = propertyType.GetConstructor(Type.EmptyTypes).Invoke(null);
                            var addFunc = propertyType.GetMethod("Add");
                            while (reader.Read())
                            {
                                var id = (uint)reader[0];
                                var data = ObjectWithID.GetByID(id, relatedType.GUID);
                                addFunc.Invoke(result, new object[] { data });
                            }
                            return result;
                        }
                    }
            }
            throw new ReflectionException();
        }

        public object GetDictionary(ObjectWithID obj, string propertyName)
        {
            throw new NotImplementedException();
        }
    }
}
