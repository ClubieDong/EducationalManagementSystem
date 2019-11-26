using EducationalManagementSystem.Client.Models.CourseModels;
using EducationalManagementSystem.Client.Models.HierarchyModels;
using EducationalManagementSystem.Client.Models.UserModels;
using EducationalManagementSystem.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalManagementSystem.Client.Models.ApplicationModels
{
    public abstract class Application : ObjectWithID
    {
        public static Dictionary<uint, Application> ApplicationList { get; } = new Dictionary<uint, Application>();

        public enum AuditState
        {
            Auditing,
            Approved,
            Disapproved,
            Cancelled
        }

        private User _Applicant;
        public User Applicant
        {
            get
            {
                if (ID.HasValue && _Applicant == null)
                    _Applicant = (User)DataServiceFactory.DataService.GetValue(this, nameof(Applicant));
                return _Applicant;
            }
            set
            {
                if (_Applicant == value)
                    return;
                _Applicant = value;
                if (!ID.HasValue)
                    return;
                DataServiceFactory.DataService.SetValue(this, nameof(Applicant), value);
            }
        }

        private Administrator _Auditor;
        public Administrator Auditor
        {
            get
            {
                if (ID.HasValue && _Auditor == null)
                    _Auditor = (Administrator)DataServiceFactory.DataService.GetValue(this, nameof(Auditor));
                return _Auditor;
            }
            set
            {
                if (_Auditor == value)
                    return;
                _Auditor = value;
                if (!ID.HasValue)
                    return;
                DataServiceFactory.DataService.SetValue(this, nameof(Auditor), value);
            }
        }

        private AuditState? _State;
        public AuditState? State
        {
            get
            {
                if (ID.HasValue && _State == null)
                    _State = (AuditState)DataServiceFactory.DataService.GetValue(this, nameof(State));
                return _State;
            }
            set
            {
                if (_State == value)
                    return;
                _State = value;
                if (!ID.HasValue)
                    return;
                DataServiceFactory.DataService.SetValue(this, nameof(State), value);
            }
        }

        public void Approve(Administrator admin)
        {
            Auditor = admin;
            State = AuditState.Approved;
            OnApproved();
        }
        public void Disapprove(Administrator admin)
        {
            Auditor = admin;
            State = AuditState.Disapproved;
        }
        public void Cancel()
        {
            State = AuditState.Cancelled;
        }
        public virtual void OnApproved() { }
    }

    public class AddCourseApplication : Application
    {
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

        private Course.PublicityType? _Publicity;
        public Course.PublicityType? Publicity
        {
            get
            {
                if (ID.HasValue && _Publicity == null)
                    _Publicity = (Course.PublicityType)DataServiceFactory.DataService.GetValue(this, nameof(Publicity));
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

        private string _Description;
        public string Description
        {
            get
            {
                if (ID.HasValue && _Description == null)
                    _Description = (string)DataServiceFactory.DataService.GetValue(this, nameof(Description));
                return _Description;
            }
            set
            {
                if (_Description == value)
                    return;
                _Description = value;
                if (!ID.HasValue)
                    return;
                DataServiceFactory.DataService.SetValue(this, nameof(Description), value);
            }
        }

        public override void OnApproved()
        {
            var course = (Course)DataServiceFactory.DataService.NewObject(typeof(Course));
            course.CourseID = CourseID;
            course.Name = Name;
            course.Credit = Credit;
            course.Major = Major;
            course.Publicity = Publicity;
            course.Description = Description;
        }
    }
}
