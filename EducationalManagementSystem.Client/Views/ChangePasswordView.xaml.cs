using EducationalManagementSystem.Client.ViewModels;
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

namespace EducationalManagementSystem.Client.Views
{
    /// <summary>
    /// ChangePasswordView.xaml 的交互逻辑
    /// </summary>
    public partial class ChangePasswordView : UserControl
    {
        public ChangePasswordView()
        {
            InitializeComponent();
        }

        private void VisibilityChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var vm = (ChangePasswordViewModel)DataContext;
            if (Visibility == Visibility.Visible)
                vm.Show();
            else
            {
                oldPwd.Password = string.Empty;
                newPwd.Password = string.Empty;
                confirmPwd.Password = string.Empty;
                vm.Hide();
            }
        }
    }
}
