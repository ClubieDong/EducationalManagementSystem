using EducationalManagementSystem.Client.Models;
using EducationalManagementSystem.Client.Models.HierarchyModels;
using EducationalManagementSystem.Client.Models.UserModels;
using EducationalManagementSystem.Client.Services;
using EducationalManagementSystem.Client.ViewModels;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EducationalManagementSystem.Client
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var vm = (MainWindowViewModel)DataContext;
            vm.LoginVM = (LoginViewModel)loginView.DataContext;
            vm.ViewPersonalInfoVM = (ViewPersonalInfoViewModel)viewPersonalInfoView.DataContext;
            vm.ChangePasswordVM = (ChangePasswordViewModel)changePasswordView.DataContext;
            vm.AddCourseVM = (AddCourseViewModel)addCourseView.DataContext;
        }
    }
}
