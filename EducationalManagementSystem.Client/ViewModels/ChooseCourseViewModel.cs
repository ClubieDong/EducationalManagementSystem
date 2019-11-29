using EducationalManagementSystem.Client.Models.CourseModels;
using EducationalManagementSystem.Client.Models.UserModels;
using EducationalManagementSystem.Client.Services;
using EducationalManagementSystem.Client.Services.Exceptions;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace EducationalManagementSystem.Client.ViewModels
{
    public class ChooseCourseViewModel : BindableBase
    {
        public ChooseCourseViewModel()
        {
            CheckCommand = new DelegateCommand<Class>(Check, CanCheck);
            UncheckCommand = new DelegateCommand<Class>(Uncheck, CanUncheck);
            ConfirmCommand = new DelegateCommand(Confirm, CanConfirm);
        }

        public MainWindowViewModel MainVM { get; set; }

        private List<Class> _ClassList;
        public List<Class> ClassList
        {
            get => _ClassList;
            set => SetProperty(ref _ClassList, value);
        }

        private List<Class> _ChosenClassList;
        public List<Class> ChosenClassList
        {
            get => _ChosenClassList;
            set => SetProperty(ref _ChosenClassList, value);
        }

        public ICommand CheckCommand { get; }
        private void Check(Class c)
        {
            if (c == null)
                return;
            ChosenClassList.Add(c);
        }
        private bool CanCheck(Class c) => true;

        public ICommand UncheckCommand { get; }
        private void Uncheck(Class c)
        {
            if (c == null)
                return;
            ChosenClassList.Remove(c);
        }
        private bool CanUncheck(Class c) => true;

        public ICommand ConfirmCommand { get; }
        private void Confirm()
        {
            try
            {
                ChooseCourseServiceFactory.ChooseCourseService.ChooseCourse((Student)MainVM.User, ChosenClassList);
                RaisePropertyChanged(nameof(ClassList));
                MessageBox.Show("选课成功！");
            }
            catch (ActivityOverlapException)
            {
                MessageBox.Show("课程时间冲突！");
            }
        }
        private bool CanConfirm() => true;

        public void Show()
        {
            if (MainVM.User == null)
                return;
            DataServiceFactory.DataService.GetAllObjects(typeof(Class));
            ClassList = Class.ClassList
                .Select(n => n.Value)
                .Where(n =>
                {
                    if (DateTime.Now < n.ChooseStartTime || n.ChooseEndTime < DateTime.Now)
                        return false;
                    if (n.Course.Publicity == Course.PublicityType.University)
                        return true;
                    if (n.Course.Publicity == Course.PublicityType.College && ((Student)MainVM.User).Major.College == n.Course.Major.College)
                        return true;
                    if (n.Course.Publicity == Course.PublicityType.Major && ((Student)MainVM.User).Major == n.Course.Major)
                        return true;
                    return false;
                })
                .ToList();
            ChosenClassList = new List<Class>();
        }
    }
}
