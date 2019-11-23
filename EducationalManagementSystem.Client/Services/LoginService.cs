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
            // 检查用户是否存在
            using (var cmd = DatabaseDataService.Connection.CreateCommand())
            {
                cmd.CommandText = $"SELECT COUNT(*) FROM User WHERE UserID = @UserID";
                cmd.Parameters.Add("@UserID", MySqlDbType.TinyText);
                cmd.Parameters["@UserID"].Value = userID;
                using (var reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    if ((long)reader[0] == 0)
                        throw new NoUserIDException();
                }
            }
            // 检查密码是否正确
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            byte[] sha256 = SHA256.Create().ComputeHash(bytes);
            using (var cmd = DatabaseDataService.Connection.CreateCommand())
            {
                cmd.CommandText = $"SELECT Password = @Password FROM User WHERE UserID = @UserID";
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
            }
            // 获取用户对象的ID
            using (var cmd = DatabaseDataService.Connection.CreateCommand())
            {
                cmd.CommandText = $"SELECT ID, Type FROM User WHERE UserID = @UserID";
                cmd.Parameters.Add("@UserID", MySqlDbType.TinyText);
                cmd.Parameters["@UserID"].Value = userID;
                using (var reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    var id = (uint)reader["ID"];
                    var guid = new Guid((byte[])reader["Type"]);
                    return User.GetByID(id, guid);
                }
            }
        }
    }
}
