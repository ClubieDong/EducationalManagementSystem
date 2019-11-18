﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.IO;
using System.Reflection;
using EducationalManagementSystem.Client.Models;

namespace EducationalManagementSystem.Client.Services
{
    public interface IDataService
    {
        object GetValue(ObjectWithID obj, PropertyInfo property);
        void SetValue(ObjectWithID obj, PropertyInfo property, object value);
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
        };

        public object GetValue(ObjectWithID obj, PropertyInfo property)
        {
            var type = property.DeclaringType;
            var field = type.GetField($"_{property.Name}", BindingFlags.NonPublic | BindingFlags.Instance);
            if (obj.ID.HasValue && field.GetValue(obj) == null)
                using (var cmd = Connection.CreateCommand())
                {
                    cmd.CommandText = $"SELECT {property.Name} FROM {type.Name} WHERE ID = {obj.ID}";
                    using (var reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        field.SetValue(obj, reader[0]);
                    }
                }
            return field.GetValue(obj);
        }

        public void SetValue(ObjectWithID obj, PropertyInfo property, object value)
        {
            var type = property.DeclaringType;
            var field = type.GetField($"_{property.Name}", BindingFlags.NonPublic | BindingFlags.Instance);
            if (field.GetValue(obj) == value)
                return;
            field.SetValue(obj, value);
            if (!obj.ID.HasValue)
                return;
            using (var cmd = Connection.CreateCommand())
            {
                cmd.CommandText = $"UPDATE {type.Name} SET {property.Name} = @value WHERE ID = {obj.ID}";
                cmd.Parameters.Add("@value", _TypeToDbType[property.PropertyType]);
                cmd.Parameters["@value"].Value = value;
                cmd.ExecuteNonQuery();
            }
        }
    }
}
