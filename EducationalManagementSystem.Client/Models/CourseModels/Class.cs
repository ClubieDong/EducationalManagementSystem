using EducationalManagementSystem.Client.Models.UserModels;
using EducationalManagementSystem.Client.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalManagementSystem.Client.Models.CourseModels
{
    public class Class : ObjectWithID
    {
        public static Dictionary<uint, Class> ClassList { get; } = new Dictionary<uint, Class>();

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

        private Teacher _Teacher;
        public Teacher Teacher
        {
            get
            {
                if (ID.HasValue && _Teacher == null)
                    _Teacher = (Teacher)DataServiceFactory.DataService.GetValue(this, nameof(Teacher));
                return _Teacher;
            }
            set
            {
                if (_Teacher == value)
                    return;
                _Teacher = value;
                if (!ID.HasValue)
                    return;
                DataServiceFactory.DataService.SetValue(this, nameof(Teacher), value);
            }
        }

        private DateTime? _ChooseStartTime;
        public DateTime? ChooseStartTime
        {
            get
            {
                if (ID.HasValue && _ChooseStartTime == null)
                    _ChooseStartTime = (DateTime)DataServiceFactory.DataService.GetValue(this, nameof(ChooseStartTime));
                return _ChooseStartTime;
            }
            set
            {
                if (_ChooseStartTime == value)
                    return;
                _ChooseStartTime = value;
                if (!ID.HasValue)
                    return;
                DataServiceFactory.DataService.SetValue(this, nameof(ChooseStartTime), value);
            }
        }

        private DateTime? _ChooseEndTime;
        public DateTime? ChooseEndTime
        {
            get
            {
                if (ID.HasValue && _ChooseEndTime == null)
                    _ChooseEndTime = (DateTime)DataServiceFactory.DataService.GetValue(this, nameof(ChooseEndTime));
                return _ChooseEndTime;
            }
            set
            {
                if (_ChooseEndTime == value)
                    return;
                _ChooseEndTime = value;
                if (!ID.HasValue)
                    return;
                DataServiceFactory.DataService.SetValue(this, nameof(ChooseEndTime), value);
            }
        }

        private List<Student> _StudentList;
        public List<Student> StudentList
        {
            get
            {
                if (ID.HasValue && _StudentList == null)
                    _StudentList = (List<Student>)DataServiceFactory.DataService.GetList(this, nameof(StudentList));
                return _StudentList;
            }
        }

        private List<Lesson> _LessonList;
        public List<Lesson> LessonList
        {
            get
            {
                if (ID.HasValue && _LessonList == null)
                    _LessonList = (List<Lesson>)DataServiceFactory.DataService.GetList(this, nameof(LessonList));
                return _LessonList;
            }
        }

        private List<Examination> _ExaminationList;
        public List<Examination> ExaminationList
        {
            get
            {
                if (ID.HasValue && _ExaminationList == null)
                    _ExaminationList = (List<Examination>)DataServiceFactory.DataService.GetList(this, nameof(ExaminationList));
                return _ExaminationList;
            }
        }
    }
}
