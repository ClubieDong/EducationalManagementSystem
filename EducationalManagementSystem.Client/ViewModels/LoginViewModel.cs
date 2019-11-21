using EducationalManagementSystem.Client.Models.UserModels;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public event Func<User> OnLoggedIn;

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

        }
        private bool CanLogin(PasswordBox passwordBox) => true;
        #endregion
    }
}
