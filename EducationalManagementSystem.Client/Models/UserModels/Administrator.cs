using EducationalManagementSystem.Client.Models.HierarchyModels;
using EducationalManagementSystem.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalManagementSystem.Client.Models.UserModels
{
    public abstract class Administrator : User
    {
    }

    public class CollegeAdministrator : Administrator
    {
        private College _College;
        public College College
        {
            get
            {
                if (ID.HasValue && _College == null)
                    _College = (College)DataServiceFactory.DataService.GetValue(this, nameof(College));
                return _College;
            }
            set
            {
                if (_College == value)
                    return;
                _College = value;
                if (!ID.HasValue)
                    return;
                DataServiceFactory.DataService.SetValue(this, nameof(College), value);
            }
        }
    }

    public class UniversityAdministrator : Administrator
    {
    }
}
