using EducationalManagementSystem.Client.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace EducationalManagementSystem.Client.Services
{
    public interface IDataService
    {
        ObjectWithID CreateObject(uint id, Type type);
        object GetValue(ObjectWithID obj, string propertyName);
        void SetValue(ObjectWithID obj, string propertyName, object value);
        ObjectWithID NewObject(Type type);
        object GetList(ObjectWithID obj, string propertyName);
        object GetDictionary(ObjectWithID obj, string propertyName);
        void GetAllObjects(Type type);
        bool CheckUniqueness(PropertyInfo property, object value);
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
        private static MySqlCommand _Command;
        public static MySqlCommand Command
        {
            get
            {
                if (_Command == null)
                    _Command = Connection.CreateCommand();
                return _Command;
            }
        }

        private static readonly Dictionary<Type, MySqlDbType> _TypeToDbType = new Dictionary<Type, MySqlDbType>()
        {
            { typeof(uint?), MySqlDbType.UInt32 },
            { typeof(double?), MySqlDbType.Double },
            { typeof(string), MySqlDbType.Text },
            { typeof(DateTime?), MySqlDbType.DateTime },
        };

        public ObjectWithID CreateObject(uint id, Type type)
        {
            var baseType = type;
            while (baseType.BaseType != typeof(ObjectWithID))
                baseType = baseType.BaseType;
            Command.CommandText = $"SELECT Type FROM {baseType.Name} WHERE ID = {id}";
            using (var reader = Command.ExecuteReader())
            {
                reader.Read();
                var guid = new Guid((byte[])reader[0]);
                return ObjectWithID.GetByID(id, guid);
            }
        }

        public object GetValue(ObjectWithID obj, string propertyName)
        {
            var property = obj.GetType().GetProperty(propertyName);
            var declaringType = property.DeclaringType;
            var propertyType = property.PropertyType;
            // ObjectWithID子类
            if (propertyType.IsSubclassOf(typeof(ObjectWithID)))
            {
                uint id;
                Command.CommandText = $"SELECT {propertyName} FROM {declaringType.Name} WHERE ID = {obj.ID}";
                using (var reader = Command.ExecuteReader())
                {
                    reader.Read();
                    id = (uint)reader[0];
                }
                return CreateObject(id, propertyType);
            }
            // Nullable<Enum>
            if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>) && propertyType.GenericTypeArguments[0].IsEnum)
            {
                Command.CommandText = $"SELECT {propertyName} FROM {declaringType.Name} WHERE ID = {obj.ID}";
                using (var reader = Command.ExecuteReader())
                {
                    reader.Read();
                    return Enum.Parse(propertyType.GenericTypeArguments[0], (string)reader[0]);
                }
            }
            // 一般类型
            Command.CommandText = $"SELECT {propertyName} FROM {declaringType.Name} WHERE ID = {obj.ID}";
            using (var reader = Command.ExecuteReader())
            {
                reader.Read();
                return reader[0];
            }
        }

        public void SetValue(ObjectWithID obj, string propertyName, object value)
        {
            var property = obj.GetType().GetProperty(propertyName);
            var declaringType = property.DeclaringType;
            var propertyType = property.PropertyType;
            Command.CommandText = $"UPDATE {declaringType.Name} SET {property.Name} = @Value WHERE ID = {obj.ID}";
            Command.Parameters.Clear();
            // ObjectWithID子类
            if (value is ObjectWithID data)
            {
                Command.Parameters.Add("@Value", MySqlDbType.UInt32);
                Command.Parameters["@Value"].Value = data.ID;
            }
            // Nullable<Enum>
            else if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>) && propertyType.GenericTypeArguments[0].IsEnum)
            {
                Command.Parameters.Add("@Value", MySqlDbType.Text);
                Command.Parameters["@Value"].Value = value.ToString();
            }
            // 一般类型
            else
            {
                Command.Parameters.Add("@Value", _TypeToDbType[property.PropertyType]);
                Command.Parameters["@Value"].Value = value;
            }
            Command.ExecuteNonQuery();
        }

        public ObjectWithID NewObject(Type type)
        {
            var baseType = type;
            while (baseType.BaseType != typeof(ObjectWithID))
                baseType = baseType.BaseType;
            Command.CommandText = $"INSERT INTO {baseType.Name} (Type) VALUES (@Type)";
            Command.Parameters.Clear();
            Command.Parameters.Add("@Type", MySqlDbType.Binary);
            Command.Parameters["@Type"].Value = type.GUID.ToByteArray();
            Command.ExecuteNonQuery();
            uint id;
            Command.CommandText = $"SELECT LAST_INSERT_ID()";
            using (var reader = Command.ExecuteReader())
            {
                reader.Read();
                id = (uint)(ulong)reader[0];
            }
            var tempType = type;
            while (tempType != baseType)
            {
                Command.CommandText = $"INSERT INTO {tempType.Name} (ID) VALUES (@ID)";
                Command.Parameters.Clear();
                Command.Parameters.Add("@ID", MySqlDbType.UInt32);
                Command.Parameters["@ID"].Value = id;
                Command.ExecuteNonQuery();
                tempType = tempType.BaseType;
            }
            return CreateObject(id, type);
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
                if (objType == relatedPropertyType || objType.IsSubclassOf(relatedPropertyType))
                {
                    var idList = new List<uint>();
                    Command.CommandText = $"SELECT ID FROM {relatedType.Name} WHERE {relatedProperty.Name} = {obj.ID}";
                    using (var reader = Command.ExecuteReader())
                        while (reader.Read())
                            idList.Add((uint)reader[0]);
                    var result = propertyType.GetConstructor(Type.EmptyTypes).Invoke(null);
                    var addFunc = propertyType.GetMethod("Add");
                    foreach (var id in idList)
                    {
                        var data = CreateObject(id, relatedType);
                        addFunc.Invoke(result, new object[] { data });
                    }
                    return result;
                }
                // 多对多
                if (relatedPropertyType.IsGenericType && relatedPropertyType.GetGenericTypeDefinition() == typeof(List<>) && (objType == relatedPropertyType || objType.IsSubclassOf(relatedPropertyType.GenericTypeArguments[0])))
                {
                    var first = objType.Name;
                    var second = relatedType.Name;
                    if (string.Compare(first, second) > 0)
                    {
                        var temp = first;
                        first = second;
                        second = temp;
                    }
                    var idList = new List<uint>();
                    Command.CommandText = $"SELECT {relatedType.Name}ID FROM {first}_{second} WHERE {objType.Name} = {obj.ID}";
                    using (var reader = Command.ExecuteReader())
                        while (reader.Read())
                            idList.Add((uint)reader[0]);
                    var result = propertyType.GetConstructor(Type.EmptyTypes).Invoke(null);
                    var addFunc = propertyType.GetMethod("Add");
                    foreach (var id in idList)
                    {
                        var data = CreateObject(id, relatedType);
                        addFunc.Invoke(result, new object[] { data });
                    }
                    return result;
                }
            }
            return null;
            //throw new ReflectionException();
        }

        public object GetDictionary(ObjectWithID obj, string propertyName)
        {
            throw new NotImplementedException();
        }

        public void GetAllObjects(Type type)
        {
            var idList = new List<uint>();
            Command.CommandText = $"SELECT ID FROM {type.Name}";
            using (var reader = Command.ExecuteReader())
                while (reader.Read())
                    idList.Add((uint)reader[0]);
            foreach (var id in idList)
                CreateObject(id, type);
        }

        public bool CheckUniqueness(PropertyInfo property, object value)
        {
            Command.CommandText = $"SELECT COUNT(*) FROM {property.DeclaringType.Name} WHERE {property.Name} = @Value";
            Command.Parameters.Clear();
            Command.Parameters.Add("@Value", _TypeToDbType[property.PropertyType]);
            Command.Parameters["@Value"].Value = value;
            using (var reader = Command.ExecuteReader())
            {
                reader.Read();
                var count = (long)reader[0];
                return count == 0;
            }
        }
    }
}
