﻿using EducationalManagementSystem.Client.Models.HierarchyModels;
using EducationalManagementSystem.Client.Services;
using System.Collections.Generic;

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

        private Score.ScoreType? _ScoreType;
        public Score.ScoreType? ScoreType
        {
            get
            {
                if (ID.HasValue && _ScoreType == null)
                    _ScoreType = (Score.ScoreType)DataServiceFactory.DataService.GetValue(this, nameof(ScoreType));
                return _ScoreType;
            }
            set
            {
                if (_ScoreType == value)
                    return;
                _ScoreType = value;
                if (!ID.HasValue)
                    return;
                DataServiceFactory.DataService.SetValue(this, nameof(ScoreType), value);
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

        public override string ToString()
        {
            return $"{CourseID}  {Name}";
        }
    }
}
