using EducationalManagementSystem.Client.Models.CourseModels;
using EducationalManagementSystem.Client.Services;
using System;

namespace EducationalManagementSystem.Client.Models.ApplicationModels
{
    public class AddExaminationApplication : Application
    {
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

        private double? _Proportion;
        public double? Proportion
        {
            get
            {
                if (ID.HasValue && _Proportion == null)
                    _Proportion = (double)DataServiceFactory.DataService.GetValue(this, nameof(Proportion));
                return _Proportion;
            }
            set
            {
                if (_Proportion == value)
                    return;
                _Proportion = value;
                if (!ID.HasValue)
                    return;
                DataServiceFactory.DataService.SetValue(this, nameof(Proportion), value);
            }
        }

        public override void OnApproved()
        {
            var examination = (Examination)DataServiceFactory.DataService.NewObject(typeof(Examination));
            examination.Classroom = Classroom;
            examination.StartTime = StartTime;
            examination.EndTime = EndTime;
            examination.Class = Class;
            examination.Name = Name;
            examination.Proportion = Proportion;
        }
    }
}
