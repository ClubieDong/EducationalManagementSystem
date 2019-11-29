using EducationalManagementSystem.Client.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace EducationalManagementSystem.Client.Views
{
    /// <summary>
    /// ChooseCourseView.xaml 的交互逻辑
    /// </summary>
    public partial class ChooseCourseView : UserControl
    {
        public ChooseCourseView()
        {
            InitializeComponent();
        }

        private void VisibilityChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var vm = (ChooseCourseViewModel)DataContext;
            if (Visibility == Visibility.Visible)
                vm.Show();
        }
    }
}
