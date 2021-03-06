﻿using EducationalManagementSystem.Client.Models.CourseModels;
using EducationalManagementSystem.Client.Services;
using System.Collections.Generic;

namespace EducationalManagementSystem.Client.Models.HierarchyModels
{
    public class Major : ObjectWithID
    {
        public static Dictionary<uint, Major> MajorList { get; } = new Dictionary<uint, Major>();

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

        private List<Course> _CourseList;
        public List<Course> CourseList
        {
            get
            {
                if (ID.HasValue && _CourseList == null)
                    _CourseList = (List<Course>)DataServiceFactory.DataService.GetList(this, nameof(CourseList));
                return _CourseList;
            }
        }

        public override string ToString()
        {
            return $"{Number}专  {Name}";
        }
    }
}
