using EducationalManagementSystem.Client.Models.UserModels;
using EducationalManagementSystem.Client.Services.Exceptions;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.Text;

namespace EducationalManagementSystem.Client.Services
{
    public interface IChangePasswordService
    {
        void ChangePassword(User user, string oldPassword, string newPassword);
        void ResetPassword(User user);
    }

    public class ChangePasswordServiceFactory
    {
        public static IChangePasswordService ChangePasswordService { get; } = new DatabaseChangePasswordService();
    }

    public class DatabaseChangePasswordService : IChangePasswordService
    {
        public void ChangePassword(User user, string oldPassword, string newPassword)
        {
            var cmd = DatabaseDataService.Command;
            // 检查密码是否正确
            byte[] bytes = Encoding.UTF8.GetBytes(oldPassword);
            byte[] sha256 = SHA256.Create().ComputeHash(bytes);
            cmd.CommandText = $"SELECT Password = @Password FROM User WHERE ID = {user.ID}";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@Password", MySqlDbType.Binary);
            cmd.Parameters["@Password"].Value = sha256;
            using (var reader = cmd.ExecuteReader())
            {
                reader.Read();
                if ((long)reader[0] == 0)
                    throw new WrongPasswordException();
            }
            // 修改密码
            bytes = Encoding.UTF8.GetBytes(newPassword);
            sha256 = SHA256.Create().ComputeHash(bytes);
            cmd.CommandText = $"UPDATE User SET Password = @Password WHERE ID = {user.ID}";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@Password", MySqlDbType.Binary);
            cmd.Parameters["@Password"].Value = sha256;
            cmd.ExecuteNonQuery();
        }

        public void ResetPassword(User user)
        {
            var cmd = DatabaseDataService.Command;
            // 重置密码为用户ID
            var password = user.UserID;
            var bytes = Encoding.UTF8.GetBytes(password);
            var sha256 = SHA256.Create().ComputeHash(bytes);
            cmd.CommandText = $"UPDATE User SET Password = @Password WHERE ID = {user.ID}";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@Password", MySqlDbType.Binary);
            cmd.Parameters["@Password"].Value = sha256;
            cmd.ExecuteNonQuery();
        }
    }
}
