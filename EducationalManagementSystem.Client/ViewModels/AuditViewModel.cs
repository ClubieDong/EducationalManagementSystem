using EducationalManagementSystem.Client.Models.ApplicationModels;
using EducationalManagementSystem.Client.Models.UserModels;
using EducationalManagementSystem.Client.Services;
using EducationalManagementSystem.Client.Services.Exceptions;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Application = EducationalManagementSystem.Client.Models.ApplicationModels.Application;

namespace EducationalManagementSystem.Client.ViewModels
{
    public class AuditViewModel : BindableBase
    {
        public AuditViewModel()
        {
            ApproveCommand = new DelegateCommand<Application>(Approve, CanApprove);
            DisapproveCommand = new DelegateCommand<Application>(Disapprove, CanDisapprove);
        }

        public MainWindowViewModel MainVM { get; set; }

        public List<Application> ApplicationList
        {
            get => Application.ApplicationList.Select(n => n.Value).ToList();
        }

        private bool HasPermission(Application application)
        {
            if (MainVM.User is CollegeAdministrator admin)
            {
                if (application.Applicant is Student student)
                    return student.Major.College == admin.College;
                if (application.Applicant is Teacher teacher)
                    return teacher.Major.College == admin.College;
                return false;
            }
            return true;
        }

        public ICommand ApproveCommand { get; }
        private void Approve(Application application)
        {
            try
            {
                application.Approve((Administrator)MainVM.User);
                RaisePropertyChanged(nameof(ApplicationList));
                CommandManager.InvalidateRequerySuggested();
            }
            catch (IDDuplicatedException)
            {
                MessageBox.Show("ID重复，审核无法通过！");
            }
        }
        private bool _CanApprove = false;
        private bool CanApprove(Application application)
        {
            if (application != null)
                _CanApprove = application.State == Application.AuditState.Auditing && HasPermission(application);
            return _CanApprove;
        }

        public ICommand DisapproveCommand { get; }
        private void Disapprove(Application application)
        {
            application.Disapprove((Administrator)MainVM.User);
            RaisePropertyChanged(nameof(ApplicationList));
            CommandManager.InvalidateRequerySuggested();
        }
        private bool _CanDisapprove;
        private bool CanDisapprove(Application application)
        {
            if (application != null)
                _CanDisapprove = application.State == Application.AuditState.Auditing && HasPermission(application);
            return _CanDisapprove;
        }

        public void Show()
        {
            DataServiceFactory.DataService.GetAllObjects(typeof(Application));
            RaisePropertyChanged(nameof(ApplicationList));
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
