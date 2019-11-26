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
    public abstract class Student : User
    {
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

        private int? _EnrollmentYear;
        public int? EnrollmentYear
        {
            get
            {
                if (ID.HasValue && _EnrollmentYear == null)
                    _EnrollmentYear = (int)DataServiceFactory.DataService.GetValue(this, nameof(EnrollmentYear));
                return _EnrollmentYear;
            }
            set
            {
                if (_EnrollmentYear == value)
                    return;
                _EnrollmentYear = value;
                if (!ID.HasValue)
                    return;
                DataServiceFactory.DataService.SetValue(this, nameof(EnrollmentYear), value);
            }
        }

        private List<Class> _ClassList;
        public List<Class> ClassList
        {
            get
            {
                if (ID.HasValue && _ClassList == null)
                    _ClassList = (List<Class>)DataServiceFactory.DataService.GetList(this, nameof(ClassList));
                return _ClassList;
            }
        }
    }

    public class Undergraduate : Student
    {
        private string _Class;
        public string Class
        {
            get
            {
                if (ID.HasValue && _Class == null)
                    _Class = (string)DataServiceFactory.DataService.GetValue(this, nameof(Class));
                return _Class;
            }
            set
            {
                if (_Class == value)
                    return;
                _Class = value;
                if (!ID.HasValue)
                    return;
                DataServiceFactory.DataService.SetValue(this, nameof(Class), value);
            }
        }
    }

    public class Postgraduate : Student
    {
        private Teacher _Tutor;
        public Teacher Tutor
        {
            get
            {
                if (ID.HasValue && _Tutor == null)
                    _Tutor = (Teacher)DataServiceFactory.DataService.GetValue(this, nameof(Tutor));
                return _Tutor;
            }
            set
            {
                if (_Tutor == value)
                    return;
                _Tutor = value;
                if (!ID.HasValue)
                    return;
                DataServiceFactory.DataService.SetValue(this, nameof(Tutor), value);
            }
        }
    }
}
