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

namespace EducationalManagementSystem.Client.Services
{
    public interface IDataService
    {
        object GetValue(ObjectWithID obj, PropertyInfo property);
        void SetValue(ObjectWithID obj, PropertyInfo property, object value);
        ObjectWithID NewObject(Type type);
        void RemoveObject(ObjectWithID obj);
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
            { typeof(string), MySqlDbType.Text },
            { typeof(User.GenderType?), MySqlDbType.Int32 },
        };

        public object GetValue(ObjectWithID obj, PropertyInfo property)
        {
            Console.WriteLine($"GetValue(obj:{obj},property:{property}");
            var type = property.DeclaringType;
            var field = type.GetField($"_{property.Name}", BindingFlags.NonPublic | BindingFlags.Instance);
            if (obj.ID.HasValue && field.GetValue(obj) == null)
                using (var cmd = Connection.CreateCommand())
                {
                    cmd.CommandText = $"SELECT {property.Name} FROM {type.Name} WHERE ID = {obj.ID}";
                    using (var reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        if (field.FieldType.IsGenericType && field.FieldType.GetGenericTypeDefinition() == typeof(Nullable<>) && field.FieldType.GenericTypeArguments[0].IsEnum)
                        {
                            var e = Enum.Parse(field.FieldType.GenericTypeArguments[0], (string)reader[0]);
                            field.SetValue(obj, e);
                        }
                        else
                            field.SetValue(obj, reader[0]);
                    }
                }
            return field.GetValue(obj);
        }

        public void SetValue(ObjectWithID obj, PropertyInfo property, object value)
        {
            Console.WriteLine($"SetValue(obj:{obj},property:{property},value:{value}");
            var type = property.DeclaringType;
            var field = type.GetField($"_{property.Name}", BindingFlags.NonPublic | BindingFlags.Instance);
            if (field.GetValue(obj) == value)
                return;
            field.SetValue(obj, value);
            if (!obj.ID.HasValue)
                return;
            using (var cmd = Connection.CreateCommand())
            {
                cmd.CommandText = $"UPDATE {type.Name} SET {property.Name} = @Value WHERE ID = {obj.ID}";
                cmd.Parameters.Add("@Value", _TypeToDbType[property.PropertyType]);
                cmd.Parameters["@Value"].Value = value;
                cmd.ExecuteNonQuery();
            }
        }

        public ObjectWithID NewObject(Type type)
        {
            Type baseType = type;
            while (baseType.BaseType != typeof(ObjectWithID))
                baseType = baseType.BaseType;
            using (var cmd = Connection.CreateCommand())
            {
                cmd.CommandText = $"INSERT INTO {baseType.Name} (Type) VALUES (@Type)";
                cmd.Parameters.Add("@Type", MySqlDbType.Guid);
                cmd.Parameters["@Type"].Value = type.GUID;
                cmd.ExecuteNonQuery();
            }
            using (var cmd = Connection.CreateCommand())
            {
                cmd.CommandText = $"SELECT LAST_INSERT_ID()";
                using (var reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    var types = new Type[] { typeof(uint) };
                    var paras = new object[] { reader[0] };
                    var result = (ObjectWithID)type.GetConstructor(types).Invoke(paras);
                    return result;
                }
            }
        }

        public void RemoveObject(ObjectWithID obj)
        {
            throw new NotImplementedException();
        }
    }
}
