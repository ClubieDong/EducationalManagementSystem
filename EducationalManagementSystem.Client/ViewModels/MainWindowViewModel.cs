using EducationalManagementSystem.Client.Models.UserModels;
using EducationalManagementSystem.Client.Services;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace EducationalManagementSystem.Client.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel()
        {
            LogoutCommand = new DelegateCommand(Logout, CanLogout);
            ViewPersonalInfoCommand = new DelegateCommand(ViewPersonalInfo, CanViewPersonalInfo);
            EditPersonalInfoCommand = new DelegateCommand(EditPersonalInfo, CanEditPersonalInfo);
            ChangePasswordCommand = new DelegateCommand(ChangePassword, CanChangePassword);
        }

        #region ViewModels
        private LoginViewModel _LoginVM;
        public LoginViewModel LoginVM
        {
            get => _LoginVM;
            set
            {
                _LoginVM = value;
                _LoginVM.OnLoggedIn += LoginVM_OnLoggedIn;
            }
        }

        private ViewPersonalInfoViewModel _ViewPersonalInfoVM;
        public ViewPersonalInfoViewModel ViewPersonalInfoVM
        {
            get => _ViewPersonalInfoVM;
            set
            {
                _ViewPersonalInfoVM = value;
                _ViewPersonalInfoVM.MainVM = this;
            }
        }

        private ChangePasswordViewModel _ChangePasswordVM;
        public ChangePasswordViewModel ChangePasswordVM
        {
            get => _ChangePasswordVM;
            set
            {
                _ChangePasswordVM = value;
                _ChangePasswordVM.MainVM = this;
            }
        }
        #endregion

        #region ViewState
        public enum ViewState
        {
            Login,
            Default,
            ViewPersonalInfo,
            ChangePassword
        }

        private ViewState _State = ViewState.Default;
        public ViewState State
        {
            get => _State;
            set => SetProperty(ref _State, value);
        }
        #endregion

        #region User
        private User _User = LoginServiceFactory.LoginService.Login("161810129", "Xixi");
        public User User
        {
            get => _User;
            set => SetProperty(ref _User, value);
        }
        #endregion

        #region Commands
        #region Logout
        public ICommand LogoutCommand { get; }
        public void Logout()
        {
            var result = MessageBox.Show("确认退出登录吗？", "确认退出", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                User = null;
                State = ViewState.Login;
            }
        }
        public bool CanLogout() => true;
        #endregion

        #region ViewPersonalInfo
        public ICommand ViewPersonalInfoCommand { get; }
        public void ViewPersonalInfo()
        {
            State = ViewState.ViewPersonalInfo;
        }
        public bool CanViewPersonalInfo() => true;
        #endregion

        #region EditPersonalInfo
        public ICommand EditPersonalInfoCommand { get; }
        public void EditPersonalInfo()
        {
            MessageBox.Show("修改个人信息");
        }
        public bool CanEditPersonalInfo() => true;
        #endregion 

        #region ChangePassword
        public ICommand ChangePasswordCommand { get; }
        public void ChangePassword()
        {
            State = ViewState.ChangePassword;
        }
        public bool CanChangePassword() => true;
        #endregion 
        #endregion

        private void LoginVM_OnLoggedIn(User user)
        {
            User = user;
            State = ViewState.Default;
        }
    }
}
