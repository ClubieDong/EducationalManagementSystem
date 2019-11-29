using EducationalManagementSystem.Client.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace EducationalManagementSystem.Client.Views
{
    /// <summary>
    /// InputScoreView.xaml 的交互逻辑
    /// </summary>
    public partial class InputScoreView : UserControl
    {
        public InputScoreView()
        {
            InitializeComponent();
        }

        private void VisibilityChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var vm = (InputScoreViewModel)DataContext;
            if (Visibility == Visibility.Visible)
                vm.Show();
        }
    }
}
