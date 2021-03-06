﻿using EducationalManagementSystem.Client.Services;
using System.Collections.Generic;

namespace EducationalManagementSystem.Client.Models.CourseModels
{
    public class Classroom : ObjectWithID
    {
        public static Dictionary<uint, Classroom> ClassroomList { get; } = new Dictionary<uint, Classroom>();

        private string _ClassroomID;
        public string ClassroomID
        {
            get
            {
                if (ID.HasValue && _ClassroomID == null)
                    _ClassroomID = (string)DataServiceFactory.DataService.GetValue(this, nameof(ClassroomID));
                return _ClassroomID;
            }
            set
            {
                if (_ClassroomID == value)
                    return;
                _ClassroomID = value;
                if (!ID.HasValue)
                    return;
                DataServiceFactory.DataService.SetValue(this, nameof(ClassroomID), value);
            }
        }

        private uint? _SeatCount;
        public uint? SeatCount
        {
            get
            {
                if (ID.HasValue && _SeatCount == null)
                    _SeatCount = (uint)DataServiceFactory.DataService.GetValue(this, nameof(SeatCount));
                return _SeatCount;
            }
            set
            {
                if (_SeatCount == value)
                    return;
                _SeatCount = value;
                if (!ID.HasValue)
                    return;
                DataServiceFactory.DataService.SetValue(this, nameof(SeatCount), value);
            }
        }

        private List<Activity> _ActivityList;
        public List<Activity> ActivityList
        {
            get
            {
                if (ID.HasValue && _ActivityList == null)
                    _ActivityList = (List<Activity>)DataServiceFactory.DataService.GetList(this, nameof(ActivityList));
                return _ActivityList;
            }
        }

        public override string ToString()
        {
            return $"{ClassroomID}  {SeatCount}人";
        }
    }
}
