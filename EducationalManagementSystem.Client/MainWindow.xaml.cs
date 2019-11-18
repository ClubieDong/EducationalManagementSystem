using EducationalManagementSystem.Client.Models.UserModels;
using EducationalManagementSystem.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var user1 = User.GetByID(1);
            var user2 = User.GetByID(2);
            MessageBox.Show(user1.Name);
            MessageBox.Show(user1.UserID);
            MessageBox.Show(user2.Name);
            MessageBox.Show(user2.UserID);
            user1.Name = "董世晨";
            MessageBox.Show(user1.Name);
            MessageBox.Show(user1.Name);
            MessageBox.Show(user1.Name);
        }
    }
}
