using EducationalManagementSystem.Client.Models.ApplicationModels;
using EducationalManagementSystem.Client.Models.CourseModels;
using EducationalManagementSystem.Client.Models.HierarchyModels;
using EducationalManagementSystem.Client.Services;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Application = EducationalManagementSystem.Client.Models.ApplicationModels.Application;

namespace EducationalManagementSystem.Client.ViewModels
{
    public class AddCourseViewModel : BindableBase
    {
        public AddCourseViewModel()
        {
            SummitCommand = new DelegateCommand(Summit, CanSummit);
        }

        public MainWindowViewModel MainVM { get; set; }

        private string _CourseID;
        public string CourseID
        {
            get => _CourseID;
            set => SetProperty(ref _CourseID, value);
        }

        private string _Name;
        public string Name
        {
            get => _Name;
            set => SetProperty(ref _Name, value);
        }

        private double _Credit;
        public double Credit
        {
            get => _Credit;
            set => SetProperty(ref _Credit, value);
        }

        private College _College;
        public College College
        {
            get => _College;
            set => SetProperty(ref _College, value);
        }

        private Major _Major;
        public Major Major
        {
            get => _Major;
            set => SetProperty(ref _Major, value);
        }

        private Course.PublicityType _Publicity;
        public Course.PublicityType Publicity
        {
            get => _Publicity;
            set => SetProperty(ref _Publicity, value);
        }

        private string _Description;
        public string Description
        {
            get => _Description;
            set => SetProperty(ref _Description, value);
        }

        private List<College> _CollegeList;
        public List<College> CollegeList
        {
            get => _CollegeList;
            set => SetProperty(ref _CollegeList, value);
        }

        public ICommand SummitCommand { get; }
        private void Summit()
        {
            var app = (AddCourseApplication)DataServiceFactory.DataService.NewObject(typeof(AddCourseApplication));
            app.Applicant = MainVM.User;
            app.State = Application.AuditState.Auditing;
            app.CourseID = CourseID;
            app.Name = Name;
            app.Credit = Credit;
            app.Major = Major;
            app.Publicity = Publicity;
            app.Description = Description;
            MessageBox.Show("申请提交成功！");
        }
        private bool CanSummit() => true;

        public void Show()
        {
            DataServiceFactory.DataService.GetAllObjects(typeof(College));
            CollegeList = College.CollegeList.Select(n => n.Value).ToList();
        }
    }
}
