using EducationalManagementSystem.Client.Services;
using EducationalManagementSystem.Client.Services.Exceptions;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace EducationalManagementSystem.Client.ViewModels
{
    public class ChangePasswordViewModel : BindableBase
    {
        public ChangePasswordViewModel()
        {
            ChangePasswordCommand = new DelegateCommand<PasswordBox[]>(ChangePassword, CanChangePassword);
        }

        public MainWindowViewModel MainVM { get; set; }

        public ICommand ChangePasswordCommand { get; }
        public void ChangePassword(PasswordBox[] passwordBoxes)
        {
            try
            {
                string oldPassword = passwordBoxes[0].Password;
                string newPassword = passwordBoxes[1].Password;
                string confirmPassword = passwordBoxes[2].Password;
                if (string.IsNullOrEmpty(oldPassword))
                    MessageBox.Show("请输入原密码！");
                else if (string.IsNullOrEmpty(newPassword))
                    MessageBox.Show("请输入新密码！");
                else if (string.IsNullOrEmpty(confirmPassword))
                    MessageBox.Show("请确认密码！");
                else if (newPassword != confirmPassword)
                    MessageBox.Show("两次输入的密码不一致！");
                else
                {
                    ChangePasswordServiceFactory.ChangePasswordService.ChangePassword(MainVM.User, oldPassword, newPassword);
                    MessageBox.Show("密码修改成功！");
                }
            }
            catch (WrongPasswordException)
            {
                MessageBox.Show("原密码不正确！");
            }
            finally
            {
                passwordBoxes[0].Password = string.Empty;
                passwordBoxes[1].Password = string.Empty;
                passwordBoxes[2].Password = string.Empty;
            }
        }
        public bool CanChangePassword(PasswordBox[] passwordBoxes) => true;
    }
}
