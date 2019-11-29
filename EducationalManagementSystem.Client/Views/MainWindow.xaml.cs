using EducationalManagementSystem.Client.ViewModels;
using System.Windows;

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
            var vm = (MainWindowViewModel)DataContext;
            vm.LoginVM = (LoginViewModel)loginView.DataContext;
            vm.ViewPersonalInfoVM = (ViewPersonalInfoViewModel)viewPersonalInfoView.DataContext;
            vm.ChangePasswordVM = (ChangePasswordViewModel)changePasswordView.DataContext;
            vm.AddCourseVM = (AddCourseViewModel)addCourseView.DataContext;
            vm.AddClassVM = (AddClassViewModel)addClassView.DataContext;
            vm.AuditVM = (AuditViewModel)auditView.DataContext;
            vm.ChooseCourseVM = (ChooseCourseViewModel)chooseCourseView.DataContext;
            vm.InputScoreVM = (InputScoreViewModel)inputScoreView.DataContext;
            vm.AddExaminationVM = (AddExaminationViewModel)addExaminationView.DataContext;
        }
    }
}
