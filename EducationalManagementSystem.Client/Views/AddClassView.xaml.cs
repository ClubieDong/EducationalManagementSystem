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
    /// AddClassView.xaml 的交互逻辑
    /// </summary>
    public partial class AddClassView : UserControl
    {
        public AddClassView()
        {
            InitializeComponent();
        }

        private void VisibilityChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var vm = (AddClassViewModel)DataContext;
            if (Visibility == Visibility.Visible)
                vm.Show();
        }
    }
}
