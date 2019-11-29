using EducationalManagementSystem.Client.Models.ApplicationModels;
using EducationalManagementSystem.Client.Models.CourseModels;
using EducationalManagementSystem.Client.Models.HierarchyModels;
using EducationalManagementSystem.Client.Services;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Application = EducationalManagementSystem.Client.Models.ApplicationModels.Application;

namespace EducationalManagementSystem.Client.ViewModels
{
    public class AddCourseViewModel : BindableBase
    {
        public AddCourseViewModel()
        {
            SubmitCommand = new DelegateCommand(Submit, CanSubmit);
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

        private double? _Credit;
        public double? Credit
        {
            get => _Credit;
            set => SetProperty(ref _Credit, value);
        }

        private College _College;
        public College College
        {
            get => _College;
            set
            {
                SetProperty(ref _College, value);
                RaisePropertyChanged(nameof(MajorList));
            }
        }

        private Major _Major;
        public Major Major
        {
            get => _Major;
            set => SetProperty(ref _Major, value);
        }

        private Course.PublicityType? _Publicity;
        public Course.PublicityType? Publicity
        {
            get => _Publicity;
            set => SetProperty(ref _Publicity, value);
        }

        private Score.ScoreType? _ScoreType;
        public Score.ScoreType? ScoreType
        {
            get => _ScoreType;
            set => SetProperty(ref _ScoreType, value);
        }

        private string _Description;
        public string Description
        {
            get => _Description;
            set => SetProperty(ref _Description, value);
        }

        public List<College> CollegeList
        {
            get => College.CollegeList.Select(n => n.Value).ToList();
        }

        public List<Major> MajorList
        {
            get => Major.MajorList.Select(n => n.Value).Where(n => n.College == College).ToList();
        }

        public ICommand SubmitCommand { get; }
        private void Submit()
        {
            if (string.IsNullOrEmpty(CourseID))
                MessageBox.Show("请输入课程ID！");
            else if (string.IsNullOrEmpty(Name))
                MessageBox.Show("请输入课程名！");
            else if (Credit == null)
                MessageBox.Show("请输入合法的学分！");
            else if (Credit <= 0)
                MessageBox.Show("学分必须大于零！");
            else if (College == null)
                MessageBox.Show("请选择学院！");
            else if (Major == null)
                MessageBox.Show("请选择专业！");
            else if (Publicity == null)
                MessageBox.Show("请选择授课对象！");
            else if (!DataServiceFactory.DataService.CheckUniqueness(typeof(Course).GetProperty(nameof(CourseID)), CourseID))
                MessageBox.Show("课程ID不能重复！");
            else
            {
                var app = (AddCourseApplication)DataServiceFactory.DataService.NewObject(typeof(AddCourseApplication));
                app.Applicant = MainVM.User;
                app.State = Application.AuditState.Auditing;
                app.SubmitTime = DateTime.Now;
                app.CourseID = CourseID;
                app.Name = Name;
                app.Credit = Credit;
                app.Major = Major;
                app.Publicity = Publicity;
                app.ScoreType = ScoreType;
                app.Description = Description;
                MessageBox.Show("申请提交成功！");
            }
        }
        private bool CanSubmit() => true;

        public void Show()
        {
            DataServiceFactory.DataService.GetAllObjects(typeof(College));
            DataServiceFactory.DataService.GetAllObjects(typeof(Major));
            RaisePropertyChanged(nameof(CollegeList));
        }
    }
}
