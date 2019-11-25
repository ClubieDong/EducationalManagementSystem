using EducationalManagementSystem.Client.Models.CourseModels;
using EducationalManagementSystem.Client.Models.HierarchyModels;
using EducationalManagementSystem.Client.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalManagementSystem.Client.Models.UserModels
{
    public abstract class Teacher : User
    {
        private double? _MonthlySalary;
        public double? MonthlySalary
        {
            get
            {
                if (ID.HasValue && _MonthlySalary == null)
                    _MonthlySalary = (double)DataServiceFactory.DataService.GetValue(this, nameof(MonthlySalary));
                return _MonthlySalary;
            }
            set
            {
                if (_MonthlySalary == value)
                    return;
                _MonthlySalary = value;
                if (!ID.HasValue)
                    return;
                DataServiceFactory.DataService.SetValue(this, nameof(MonthlySalary), value);
            }
        }

        private double? _SalaryPerClass;
        public double? SalaryPerClass
        {
            get
            {
                if (ID.HasValue && _SalaryPerClass == null)
                    _SalaryPerClass = (double)DataServiceFactory.DataService.GetValue(this, nameof(SalaryPerClass));
                return _SalaryPerClass;
            }
            set
            {
                if (_SalaryPerClass == value)
                    return;
                _SalaryPerClass = value;
                if (!ID.HasValue)
                    return;
                DataServiceFactory.DataService.SetValue(this, nameof(SalaryPerClass), value);
            }
        }

        private Major _Major;
        public Major Major
        {
            get
            {
                if (ID.HasValue && _Major == null)
                    _Major = (Major)DataServiceFactory.DataService.GetValue(this, nameof(Major));
                return _Major;
            }
            set
            {
                if (_Major == value)
                    return;
                _Major = value;
                if (!ID.HasValue)
                    return;
                DataServiceFactory.DataService.SetValue(this, nameof(Major), value);
            }
        }

        private string _EducationBackground;
        public string EducationBackground
        {
            get
            {
                if (ID.HasValue && _EducationBackground == null)
                    _EducationBackground = (string)DataServiceFactory.DataService.GetValue(this, nameof(EducationBackground));
                return _EducationBackground;
            }
            set
            {
                if (_EducationBackground == value)
                    return;
                _EducationBackground = value;
                if (!ID.HasValue)
                    return;
                DataServiceFactory.DataService.SetValue(this, nameof(EducationBackground), value);
            }
        }

        private string _GraduateSchool;
        public string GraduateSchool
        {
            get
            {
                if (ID.HasValue && _GraduateSchool == null)
                    _GraduateSchool = (string)DataServiceFactory.DataService.GetValue(this, nameof(GraduateSchool));
                return _GraduateSchool;
            }
            set
            {
                if (_GraduateSchool == value)
                    return;
                _GraduateSchool = value;
                if (!ID.HasValue)
                    return;
                DataServiceFactory.DataService.SetValue(this, nameof(GraduateSchool), value);
            }
        }

        private ObservableCollection<Class> _ClassList;
        public ObservableCollection<Class> ClassList
        {
            get
            {
                if (ID.HasValue && _ClassList == null)
                    _ClassList = (ObservableCollection<Class>)DataServiceFactory.DataService.GetList(this, nameof(ClassList));
                return _ClassList;
            }
        }
    }

    public class Lecturer : Teacher
    {
    }

    public class Professor : Teacher
    {
    }
}
