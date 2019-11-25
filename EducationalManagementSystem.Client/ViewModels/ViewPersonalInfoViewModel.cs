using EducationalManagementSystem.Client.Models.UserModels;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace EducationalManagementSystem.Client.ViewModels
{
    public class ViewPersonalInfoViewModel : BindableBase
    {
        public MainWindowViewModel MainVM { get; set; }

        private User _User;
        public User User
        {
            get => _User;
            set => SetProperty(ref _User, value);
        }

        public void Show()
        {
            User = MainVM.User;
        }
        public void Hide()
        {
            User = null;
        }
    }
}
