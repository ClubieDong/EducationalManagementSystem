using EducationalManagementSystem.Client.Models.UserModels;
using EducationalManagementSystem.Client.Services;
using Prism.Commands;
using Prism.Mvvm;
using System.Windows;
using System.Windows.Input;

namespace EducationalManagementSystem.Client.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel()
        {
            LogoutCommand = new DelegateCommand(Logout, CanLogout);
            ViewPersonalInfoCommand = new DelegateCommand(ViewPersonalInfo, CanViewPersonalInfo);
            EditPersonalInfoCommand = new DelegateCommand(EditPersonalInfo, CanEditPersonalInfo);
            ChangePasswordCommand = new DelegateCommand(ChangePassword, CanChangePassword);
            AddCourseCommand = new DelegateCommand(AddCourse, CanAddCourse);
            AddClassCommand = new DelegateCommand(AddClass, CanAddClass);
            AuditCommand = new DelegateCommand(Audit, CanAudit);
            ChooseCourseCommand = new DelegateCommand(ChooseCourse, CanChooseCourse);
            InputScoreCommand = new DelegateCommand(InputScore, CanInputScore);
            AddExaminationCommand = new DelegateCommand(AddExamination, CanAddExamination);
        }

        #region ViewModels
        private LoginViewModel _LoginVM;
        public LoginViewModel LoginVM
        {
            get => _LoginVM;
            set
            {
                _LoginVM = value;
                _LoginVM.MainVM = this;
            }
        }

        private ViewPersonalInfoViewModel _ViewPersonalInfoVM;
        public ViewPersonalInfoViewModel ViewPersonalInfoVM
        {
            get => _ViewPersonalInfoVM;
            set
            {
                _ViewPersonalInfoVM = value;
                _ViewPersonalInfoVM.MainVM = this;
            }
        }

        private ChangePasswordViewModel _ChangePasswordVM;
        public ChangePasswordViewModel ChangePasswordVM
        {
            get => _ChangePasswordVM;
            set
            {
                _ChangePasswordVM = value;
                _ChangePasswordVM.MainVM = this;
            }
        }

        private AddCourseViewModel _AddCourseVM;
        public AddCourseViewModel AddCourseVM
        {
            get => _AddCourseVM;
            set
            {
                _AddCourseVM = value;
                _AddCourseVM.MainVM = this;
            }
        }

        private AddClassViewModel _AddClassVM;
        public AddClassViewModel AddClassVM
        {
            get => _AddClassVM;
            set
            {
                _AddClassVM = value;
                _AddClassVM.MainVM = this;
            }
        }

        private AuditViewModel _AuditVM;
        public AuditViewModel AuditVM
        {
            get => _AuditVM;
            set
            {
                _AuditVM = value;
                _AuditVM.MainVM = this;
            }
        }

        private ChooseCourseViewModel _ChooseCourseVM;
        public ChooseCourseViewModel ChooseCourseVM
        {
            get => _ChooseCourseVM;
            set
            {
                _ChooseCourseVM = value;
                _ChooseCourseVM.MainVM = this;
            }
        }

        private InputScoreViewModel _InputScoreVM;
        public InputScoreViewModel InputScoreVM
        {
            get => _InputScoreVM;
            set
            {
                _InputScoreVM = value;
                _InputScoreVM.MainVM = this;
            }
        }

        private AddExaminationViewModel _AddExaminationVM;
        public AddExaminationViewModel AddExaminationVM
        {
            get => _AddExaminationVM;
            set
            {
                _AddExaminationVM = value;
                _AddExaminationVM.MainVM = this;
            }
        }
        #endregion

        #region ViewState
        public enum ViewState
        {
            Login,
            Default,
            ViewPersonalInfo,
            ChangePassword,
            AddCourse,
            AddClass,
            Audit,
            ChooseCourse,
            InputScore,
            AddExamination,
        }

        private ViewState _State = ViewState.Login;
        public ViewState State
        {
            get => _State;
            set => SetProperty(ref _State, value);
        }
        #endregion

        #region User
        private User _User;
        public User User
        {
            get => _User;
            set => SetProperty(ref _User, value);
        }
        #endregion

        #region Commands
        #region Logout
        public ICommand LogoutCommand { get; }
        public void Logout()
        {
            var result = MessageBox.Show("确认退出登录吗？", "确认退出", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                User = null;
                State = ViewState.Login;
            }
        }
        public bool CanLogout() => true;
        #endregion

        #region ViewPersonalInfo
        public ICommand ViewPersonalInfoCommand { get; }
        public void ViewPersonalInfo()
        {
            State = ViewState.ViewPersonalInfo;
        }
        public bool CanViewPersonalInfo() => true;
        #endregion

        #region EditPersonalInfo
        public ICommand EditPersonalInfoCommand { get; }
        public void EditPersonalInfo()
        {
            MessageBox.Show("修改个人信息");
        }
        public bool CanEditPersonalInfo() => true;
        #endregion 

        #region ChangePassword
        public ICommand ChangePasswordCommand { get; }
        public void ChangePassword()
        {
            State = ViewState.ChangePassword;
        }
        public bool CanChangePassword() => true;
        #endregion

        #region AddCourse
        public ICommand AddCourseCommand { get; }
        public void AddCourse()
        {
            State = ViewState.AddCourse;
        }
        public bool CanAddCourse() => true;
        #endregion

        #region AddClass
        public ICommand AddClassCommand { get; }
        public void AddClass()
        {
            State = ViewState.AddClass;
        }
        public bool CanAddClass() => true;
        #endregion

        #region Audit
        public ICommand AuditCommand { get; }
        public void Audit()
        {
            State = ViewState.Audit;
        }
        public bool CanAudit() => true;
        #endregion

        #region ChooseCourse
        public ICommand ChooseCourseCommand { get; }
        public void ChooseCourse()
        {
            State = ViewState.ChooseCourse;
        }
        public bool CanChooseCourse() => true;
        #endregion

        #region InputScore
        public ICommand InputScoreCommand { get; }
        public void InputScore()
        {
            State = ViewState.InputScore;
        }
        public bool CanInputScore() => true;
        #endregion

        #region AddExamination
        public ICommand AddExaminationCommand { get; }
        public void AddExamination()
        {
            State = ViewState.AddExamination;
        }
        public bool CanAddExamination() => true;
        #endregion
        #endregion
    }
}
