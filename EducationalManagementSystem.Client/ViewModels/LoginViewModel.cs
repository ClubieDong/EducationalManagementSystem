using EducationalManagementSystem.Client.Models.UserModels;
using EducationalManagementSystem.Client.Services;
using EducationalManagementSystem.Client.Services.Exceptions;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EducationalManagementSystem.Client.ViewModels
{
    public class LoginViewModel : BindableBase
    {
        public LoginViewModel()
        {
            LoginCommand = new DelegateCommand<PasswordBox>(Login, CanLogin);
        }

        public MainWindowViewModel MainVM { get; set; }

        public event Action<User> OnLoggedIn;

        #region UserID
        private string _UserID;
        public string UserID
        {
            get => _UserID;
            set => SetProperty(ref _UserID, value);
        }
        #endregion

        #region Login
        public ICommand LoginCommand { get; }
        private void Login(PasswordBox passwordBox)
        {
            try
            {
                if (string.IsNullOrEmpty(UserID))
                    MessageBox.Show("请输入用户名！");
                else if (string.IsNullOrEmpty(passwordBox.Password))
                    MessageBox.Show("请输入密码！");
                else
                {
                    var user = LoginServiceFactory.LoginService.Login(UserID, passwordBox.Password);
                    MainVM.User = user;
                    MainVM.State = MainWindowViewModel.ViewState.Default;
                }
            }
            catch (NoUserIDException)
            {
                MessageBox.Show("用户不存在！");
            }
            catch (WrongPasswordException)
            {
                MessageBox.Show("密码错误！");
            }
            finally
            {
                passwordBox.Password = string.Empty;
            }
        }
        private bool CanLogin(PasswordBox passwordBox) => true;
        #endregion
    }
}
