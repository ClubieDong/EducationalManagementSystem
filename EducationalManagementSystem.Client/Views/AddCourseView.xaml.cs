using EducationalManagementSystem.Client.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace EducationalManagementSystem.Client.Views
{
    /// <summary>
    /// AddCourseView.xaml 的交互逻辑
    /// </summary>
    public partial class AddCourseView : UserControl
    {
        public AddCourseView()
        {
            InitializeComponent();
        }

        private void VisibilityChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var vm = (AddCourseViewModel)DataContext;
            if (Visibility == Visibility.Visible)
                vm.Show();
        }
    }
}
