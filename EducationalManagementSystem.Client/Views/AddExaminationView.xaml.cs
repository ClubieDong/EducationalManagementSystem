using EducationalManagementSystem.Client.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace EducationalManagementSystem.Client.Views
{
    /// <summary>
    /// AddExaminationView.xaml 的交互逻辑
    /// </summary>
    public partial class AddExaminationView : UserControl
    {
        public AddExaminationView()
        {
            InitializeComponent();
        }

        private void VisibilityChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var vm = (AddExaminationViewModel)DataContext;
            if (Visibility == Visibility.Visible)
                vm.Show();
        }
    }
}
