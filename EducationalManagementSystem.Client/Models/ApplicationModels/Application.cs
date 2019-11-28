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

        private DateTime? _SubmitTime;
        public DateTime? SubmitTime
        {
            get
            {
                if (ID.HasValue && _SubmitTime == null)
                    _SubmitTime = (DateTime)DataServiceFactory.DataService.GetValue(this, nameof(SubmitTime));
                return _SubmitTime;
            }
            set
            {
                if (_SubmitTime == value)
                    return;
                _SubmitTime = value;
                if (!ID.HasValue)
                    return;
                DataServiceFactory.DataService.SetValue(this, nameof(SubmitTime), value);
            }
        }

        private DateTime? _AuditTime;
        public DateTime? AuditTime
        {
            get
            {
                if (ID.HasValue && _AuditTime == null)
                    _AuditTime = (DateTime)DataServiceFactory.DataService.GetValue(this, nameof(AuditTime));
                return _AuditTime;
            }
            set
            {
                if (_AuditTime == value)
                    return;
                _AuditTime = value;
                if (!ID.HasValue)
                    return;
                DataServiceFactory.DataService.SetValue(this, nameof(AuditTime), value);
            }
        }

        public void Approve(Administrator admin)
        {
            OnApproved();
            Auditor = admin;
            State = AuditState.Approved;
            AuditTime = DateTime.Now;
        }
        public void Disapprove(Administrator admin)
        {
            Auditor = admin;
            State = AuditState.Disapproved;
            AuditTime = DateTime.Now;
        }
        public void Cancel()
        {
            State = AuditState.Cancelled;
        }
        public virtual void OnApproved() { }
    }
}
