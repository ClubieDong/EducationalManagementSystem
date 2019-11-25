using EducationalManagementSystem.Client.Models.HierarchyModels;
using EducationalManagementSystem.Client.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalManagementSystem.Client.Models.CourseModels
{
    public class Course : ObjectWithID
    {
        public static Dictionary<uint, Course> CourseList { get; } = new Dictionary<uint, Course>();

        public enum PublicityType
        {
            Major,
            College,
            University
        }

        private string _CourseID;
        public string CourseID
        {
            get
            {
                if (ID.HasValue && _CourseID == null)
                    _CourseID = (string)DataServiceFactory.DataService.GetValue(this, nameof(CourseID));
                return _CourseID;
            }
            set
            {
                if (_CourseID == value)
                    return;
                _CourseID = value;
                if (!ID.HasValue)
                    return;
                DataServiceFactory.DataService.SetValue(this, nameof(CourseID), value);
            }
        }

        private string _Name;
        public string Name
        {
            get
            {
                if (ID.HasValue && _Name == null)
                    _Name = (string)DataServiceFactory.DataService.GetValue(this, nameof(Name));
                return _Name;
            }
            set
            {
                if (_Name == value)
                    return;
                _Name = value;
                if (!ID.HasValue)
                    return;
                DataServiceFactory.DataService.SetValue(this, nameof(Name), value);
            }
        }

        private double? _Credit;
        public double? Credit
        {
            get
            {
                if (ID.HasValue && _Credit == null)
                    _Credit = (double)DataServiceFactory.DataService.GetValue(this, nameof(Credit));
                return _Credit;
            }
            set
            {
                if (_Credit == value)
                    return;
                _Credit = value;
                if (!ID.HasValue)
                    return;
                DataServiceFactory.DataService.SetValue(this, nameof(Credit), value);
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

        private PublicityType? _Publicity;
        public PublicityType? Publicity
        {
            get
            {
                if (ID.HasValue && _Publicity == null)
                    _Publicity = (PublicityType)DataServiceFactory.DataService.GetValue(this, nameof(Publicity));
                return _Publicity;
            }
            set
            {
                if (_Publicity == value)
                    return;
                _Publicity = value;
                if (!ID.HasValue)
                    return;
                DataServiceFactory.DataService.SetValue(this, nameof(Publicity), value);
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

        private ObservableCollection<Examination> _ExaminationList;
        public ObservableCollection<Examination> ExaminationList
        {
            get
            {
                if (ID.HasValue && _ExaminationList == null)
                    _ExaminationList = (ObservableCollection<Examination>)DataServiceFactory.DataService.GetList(this, nameof(ExaminationList));
                return _ExaminationList;
            }
        }
    }
}
