using EducationalManagementSystem.Client.Models.ApplicationModels;
using EducationalManagementSystem.Client.Models.CourseModels;
using EducationalManagementSystem.Client.Models.UserModels;
using EducationalManagementSystem.Client.Services;
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
    public class AddExaminationViewModel : BindableBase
    {
        public AddExaminationViewModel()
        {
            SubmitCommand = new DelegateCommand(Submit, CanSubmit);
        }

        public MainWindowViewModel MainVM { get; set; }

        private Class _Class;
        public Class Class
        {
            get => _Class;
            set => SetProperty(ref _Class, value);
        }

        private Classroom _Classroom;
        public Classroom Classroom
        {
            get => _Classroom;
            set => SetProperty(ref _Classroom, value);
        }

        private string _Name;
        public string Name
        {
            get => _Name;
            set => SetProperty(ref _Name, value);
        }

        private double? _Proportion;
        public double? Proportion
        {
            get => _Proportion;
            set => SetProperty(ref _Proportion, value);
        }

        private DateTime? _Date;
        public DateTime? Date
        {
            get => _Date;
            set => SetProperty(ref _Date, value);
        }

        private DateTime? _StartTime;
        public DateTime? StartTime
        {
            get => _StartTime;
            set => SetProperty(ref _StartTime, value);
        }

        private DateTime? _EndTime;
        public DateTime? EndTime
        {
            get => _EndTime;
            set => SetProperty(ref _EndTime, value);
        }

        private List<Classroom> _ClassroomList;
        public List<Classroom> ClassroomList
        {
            get => _ClassroomList;
            set => SetProperty(ref _ClassroomList, value);
        }

        private List<Class> _ClassList;
        public List<Class> ClassList
        {
            get => _ClassList;
            set => SetProperty(ref _ClassList, value);
        }

        public ICommand SubmitCommand { get; }
        private void Submit()
        {
            if (Class == null)
                MessageBox.Show("请选择教学班！");
            else if (Classroom == null)
                MessageBox.Show("请选择教室！");
            else if (string.IsNullOrEmpty(Name))
                MessageBox.Show("请输入考试名称！");
            else if (Proportion ==  null)
                MessageBox.Show("请输入考试成绩占比！");
            else if (Date == null)
                MessageBox.Show("请输入考试日期！");
            else if (StartTime == null)
                MessageBox.Show("请输入考试开始时间！");
            else if (EndTime == null)
                MessageBox.Show("请输入考试结束时间！");
            else if (StartTime >= EndTime)
                MessageBox.Show("考试开始时间需要早于考试结束时间！");
            else
            {
                var app = (AddExaminationApplication)DataServiceFactory.DataService.NewObject(typeof(AddExaminationApplication));
                app.Applicant = MainVM.User;
                app.State = Application.AuditState.Auditing;
                app.SubmitTime = DateTime.Now;
                app.StartTime = Date + StartTime.Value.TimeOfDay;
                app.EndTime = Date + EndTime.Value.TimeOfDay;
                app.Classroom = Classroom;
                app.Class = Class;
                app.Name = Name;
                app.Proportion = Proportion;
                MessageBox.Show("申请提交成功！");
            }
        }
        private bool CanSubmit() => true;

        public void Show()
        {
            DataServiceFactory.DataService.GetAllObjects(typeof(Class));
            DataServiceFactory.DataService.GetAllObjects(typeof(Classroom));
            ClassList = Class.ClassList.Select(n => n.Value).Where(n => n.Teacher == (Teacher)MainVM.User).ToList();
            ClassroomList = Classroom.ClassroomList.Select(n => n.Value).ToList();
        }
    }
}
