using System.Windows;
using System.Windows.Controls;

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
            if (Visibility != Visibility.Visible)
            {
                oldPwd.Password = string.Empty;
                newPwd.Password = string.Empty;
                confirmPwd.Password = string.Empty;
            }
        }
    }
}
