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

namespace EducationalManagementSystem.Client.ViewModels
{
    public class AddCourseViewModel : BindableBase
    {
        public AddCourseViewModel()
        {
            SummitCommand = new DelegateCommand(Summit, CanSummit);
        }

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

        private List<College> _CollegeList;
        public List<College> CollegeList
        {
            get => _CollegeList;
            set => SetProperty(ref _CollegeList, value);
        }

        public ICommand SummitCommand { get; }
        private void Summit()
        {
            MessageBox.Show("Summit!");
        }
        private bool CanSummit() => true;

        public void Show()
        {
            DataServiceFactory.DataService.GetAllObjects(typeof(College));
            CollegeList = College.CollegeList.Select(n => n.Value).ToList();
        }
    }
}
