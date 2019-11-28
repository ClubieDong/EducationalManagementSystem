using EducationalManagementSystem.Client.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalManagementSystem.Client.Models.HierarchyModels
{
    public class College : ObjectWithID
    {
        public static Dictionary<uint, College> CollegeList { get; } = new Dictionary<uint, College>();

        private uint? _Number;
        public uint? Number
        {
            get
            {
                if (ID.HasValue && _Number == null)
                    _Number = (uint)DataServiceFactory.DataService.GetValue(this, nameof(Number));
                return _Number;
            }
            set
            {
                if (_Number == value)
                    return;
                _Number = value;
                if (!ID.HasValue)
                    return;
                DataServiceFactory.DataService.SetValue(this, nameof(Number), value);
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

        private List<Major> _MajorList;
        public List<Major> MajorList
        {
            get
            {
                if (ID.HasValue && _MajorList == null)
                    _MajorList = (List<Major>)DataServiceFactory.DataService.GetList(this, nameof(MajorList));
                return _MajorList;
            }
        }

        public override string ToString()
        {
            return $"{Number}院  {Name}";
        }
    }
}
