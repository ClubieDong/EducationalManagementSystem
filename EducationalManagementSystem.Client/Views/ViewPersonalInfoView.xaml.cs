using EducationalManagementSystem.Client.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace EducationalManagementSystem.Client.Views
{
    /// <summary>
    /// ViewPersonalInfoView.xaml 的交互逻辑
    /// </summary>
    public partial class ViewPersonalInfoView : UserControl
    {
        public ViewPersonalInfoView()
        {
            InitializeComponent();
        }

        private void VisiblilityChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var vm = (ViewPersonalInfoViewModel)DataContext;
            if (Visibility == Visibility.Visible)
                vm.Show();
            else
                vm.Hide();
        }
    }
}
