using EducationalManagementSystem.Client.Models.UserModels;
using EducationalManagementSystem.Client.Services;
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
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                User Clubie = LoginServiceFactory.LoginService.Login("161810129", "Xixi");
                MessageBox.Show(Clubie.Name);
                MessageBox.Show(Clubie.GetType().Name);
                User Akie = LoginServiceFactory.LoginService.Login("161810104", "DongDong");
                MessageBox.Show(Akie.Name);
                MessageBox.Show(Akie.GetType().Name);
            }
            catch (NoUserIDException)
            {
                MessageBox.Show("用户不存在！");
            }
            catch (WrongPasswordException)
            {
                MessageBox.Show("密码错误");
            }
        }
    }
}
