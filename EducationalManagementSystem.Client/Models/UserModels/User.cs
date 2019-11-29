using EducationalManagementSystem.Client.Services;
using System;
using System.Collections.Generic;

namespace EducationalManagementSystem.Client.Models.UserModels
{
    public abstract class User : ObjectWithID
    {
        public static Dictionary<uint, User> UserList { get; } = new Dictionary<uint, User>();

        public enum GenderType
        {
            Male,
            Female
        }

        private string _UserID;
        public string UserID
        {
            get
            {
                if (ID.HasValue && _UserID == null)
                    _UserID = (string)DataServiceFactory.DataService.GetValue(this, nameof(UserID));
                return _UserID;
            }
            set
            {
                if (_UserID == value)
                    return;
                _UserID = value;
                if (!ID.HasValue)
                    return;
                DataServiceFactory.DataService.SetValue(this, nameof(UserID), value);
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

        private GenderType? _Gender;
        public GenderType? Gender
        {
            get
            {
                if (ID.HasValue && _Gender == null)
                    _Gender = (GenderType)DataServiceFactory.DataService.GetValue(this, nameof(Gender));
                return _Gender;
            }
            set
            {
                if (_Gender == value)
                    return;
                _Gender = value;
                if (!ID.HasValue)
                    return;
                DataServiceFactory.DataService.SetValue(this, nameof(Gender), value);
            }
        }

        private string _IDNumber;
        public string IDNumber
        {
            get
            {
                if (ID.HasValue && _IDNumber == null)
                    _IDNumber = (string)DataServiceFactory.DataService.GetValue(this, nameof(IDNumber));
                return _IDNumber;
            }
            set
            {
                if (_IDNumber == value)
                    return;
                _IDNumber = value;
                if (!ID.HasValue)
                    return;
                DataServiceFactory.DataService.SetValue(this, nameof(IDNumber), value);
            }
        }

        private DateTime? _BirthDate;
        public DateTime? BirthDate
        {
            get
            {
                if (ID.HasValue && _BirthDate == null)
                    _BirthDate = (DateTime)DataServiceFactory.DataService.GetValue(this, nameof(BirthDate));
                return _BirthDate;
            }
            set
            {
                if (_BirthDate == value)
                    return;
                _BirthDate = value;
                if (!ID.HasValue)
                    return;
                DataServiceFactory.DataService.SetValue(this, nameof(BirthDate), value);
            }
        }

        private string _PoliticsStatus;
        public string PoliticsStatus
        {
            get
            {
                if (ID.HasValue && _PoliticsStatus == null)
                    _PoliticsStatus = (string)DataServiceFactory.DataService.GetValue(this, nameof(PoliticsStatus));
                return _PoliticsStatus;
            }
            set
            {
                if (_PoliticsStatus == value)
                    return;
                _PoliticsStatus = value;
                if (!ID.HasValue)
                    return;
                DataServiceFactory.DataService.SetValue(this, nameof(PoliticsStatus), value);
            }
        }

        private string _Nation;
        public string Nation
        {
            get
            {
                if (ID.HasValue && _Nation == null)
                    _Nation = (string)DataServiceFactory.DataService.GetValue(this, nameof(Nation));
                return _Nation;
            }
            set
            {
                if (_Nation == value)
                    return;
                _Nation = value;
                if (!ID.HasValue)
                    return;
                DataServiceFactory.DataService.SetValue(this, nameof(Nation), value);
            }
        }

        private string _NativePlace;
        public string NativePlace
        {
            get
            {
                if (ID.HasValue && _NativePlace == null)
                    _NativePlace = (string)DataServiceFactory.DataService.GetValue(this, nameof(NativePlace));
                return _NativePlace;
            }
            set
            {
                if (_NativePlace == value)
                    return;
                _NativePlace = value;
                if (!ID.HasValue)
                    return;
                DataServiceFactory.DataService.SetValue(this, nameof(NativePlace), value);
            }
        }

        private string _Telephone;
        public string Telephone
        {
            get
            {
                if (ID.HasValue && _Telephone == null)
                    _Telephone = (string)DataServiceFactory.DataService.GetValue(this, nameof(Telephone));
                return _Telephone;
            }
            set
            {
                if (_Telephone == value)
                    return;
                _Telephone = value;
                if (!ID.HasValue)
                    return;
                DataServiceFactory.DataService.SetValue(this, nameof(Telephone), value);
            }
        }

        private string _Address;
        public string Address
        {
            get
            {
                if (ID.HasValue && _Address == null)
                    _Address = (string)DataServiceFactory.DataService.GetValue(this, nameof(Address));
                return _Address;
            }
            set
            {
                if (_Address == value)
                    return;
                _Address = value;
                if (!ID.HasValue)
                    return;
                DataServiceFactory.DataService.SetValue(this, nameof(Address), value);
            }
        }
    }
}
