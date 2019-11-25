using EducationalManagementSystem.Client.Models.UserModels;
using EducationalManagementSystem.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalManagementSystem.Client.Models.CourseModels
{
    public abstract class Activity : ObjectWithID
    {
        public static Dictionary<uint, Activity> ActivityList { get; } = new Dictionary<uint, Activity>();

        private DateTime? _StartTime;
        public DateTime? StartTime
        {
            get
            {
                if (ID.HasValue && _StartTime == null)
                    _StartTime = (DateTime)DataServiceFactory.DataService.GetValue(this, nameof(StartTime));
                return _StartTime;
            }
            set
            {
                if (_StartTime == value)
                    return;
                _StartTime = value;
                if (!ID.HasValue)
                    return;
                DataServiceFactory.DataService.SetValue(this, nameof(StartTime), value);
            }
        }

        private DateTime? _EndTime;
        public DateTime? EndTime
        {
            get
            {
                if (ID.HasValue && _EndTime == null)
                    _EndTime = (DateTime)DataServiceFactory.DataService.GetValue(this, nameof(EndTime));
                return _EndTime;
            }
            set
            {
                if (_EndTime == value)
                    return;
                _EndTime = value;
                if (!ID.HasValue)
                    return;
                DataServiceFactory.DataService.SetValue(this, nameof(EndTime), value);
            }
        }

        private Classroom _Classroom;
        public Classroom Classroom
        {
            get
            {
                if (ID.HasValue && _Classroom == null)
                    _Classroom = (Classroom)DataServiceFactory.DataService.GetValue(this, nameof(Classroom));
                return _Classroom;
            }
            set
            {
                if (_Classroom == value)
                    return;
                _Classroom = value;
                if (!ID.HasValue)
                    return;
                DataServiceFactory.DataService.SetValue(this, nameof(Classroom), value);
            }
        }
    }

    public class Lesson : Activity
    {
        private Class _Class;
        public Class Class
        {
            get
            {
                if (ID.HasValue && _Class == null)
                    _Class = (Class)DataServiceFactory.DataService.GetValue(this, nameof(Class));
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

        // Todo:
        // HashSet<Student> SignedStudentList;
    }

    public class Examination : Activity
    {
        private Course _Course;
        public Course Course
        {
            get
            {
                if (ID.HasValue && _Course == null)
                    _Course = (Course)DataServiceFactory.DataService.GetValue(this, nameof(Course));
                return _Course;
            }
            set
            {
                if (_Course == value)
                    return;
                _Course = value;
                if (!ID.HasValue)
                    return;
                DataServiceFactory.DataService.SetValue(this, nameof(Course), value);
            }
        }

        private Dictionary<Student, double> _ScoreList;
        public Dictionary<Student, double> ScoreList
        {
            get
            {
                if (ID.HasValue && _ScoreList == null)
                    _ScoreList = (Dictionary<Student, double>)DataServiceFactory.DataService.GetDictionary(this, nameof(ScoreList));
                return _ScoreList;
            }
        }
    }
}
