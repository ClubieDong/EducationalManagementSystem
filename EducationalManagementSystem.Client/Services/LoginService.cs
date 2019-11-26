using EducationalManagementSystem.Client.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using System.Data;
using MySql.Data.MySqlClient;
using EducationalManagementSystem.Client.Services.Exceptions;
using EducationalManagementSystem.Client.Models;

namespace EducationalManagementSystem.Client.Services
{
    public interface ILoginService
    {
        User Login(string userID, string password);
    }

    public class LoginServiceFactory
    {
        public static ILoginService LoginService { get; } = new DatabaseLoginService();
    }

    public class DatabaseLoginService : ILoginService
    {
        public User Login(string userID, string password)
        {
            var cmd = DatabaseDataService.Command;
            // 检查用户是否存在
            cmd.CommandText = $"SELECT COUNT(*) FROM User WHERE UserID = @UserID";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@UserID", MySqlDbType.TinyText);
            cmd.Parameters["@UserID"].Value = userID;
            using (var reader = cmd.ExecuteReader())
            {
                reader.Read();
                if ((long)reader[0] == 0)
                    throw new NoUserIDException();
            }
            // 检查密码是否正确
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            byte[] sha256 = SHA256.Create().ComputeHash(bytes);
            cmd.CommandText = $"SELECT Password = @Password FROM User WHERE UserID = @UserID";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@UserID", MySqlDbType.TinyText);
            cmd.Parameters["@UserID"].Value = userID;
            cmd.Parameters.Add("@Password", MySqlDbType.Binary);
            cmd.Parameters["@Password"].Value = sha256;
            using (var reader = cmd.ExecuteReader())
            {
                reader.Read();
                if ((long)reader[0] == 0)
                    throw new WrongPasswordException();
            }
            // 获取用户对象的ID
            uint id;
            cmd.CommandText = $"SELECT ID FROM User WHERE UserID = @UserID";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@UserID", MySqlDbType.TinyText);
            cmd.Parameters["@UserID"].Value = userID;
            using (var reader = cmd.ExecuteReader())
            {
                reader.Read();
                id = (uint)reader[0];
            }
            // 创建对象，返回结果
            var user = (User)new DatabaseDataService().CreateObject(id, typeof(User));
            return user;
        }
    }
}
