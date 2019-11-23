using EducationalManagementSystem.Client.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace EducationalManagementSystem.Client.Models.UserModels
{
    public abstract class User : ObjectWithID
    {
        private static readonly Dictionary<uint, User> _UserList = new Dictionary<uint, User>();
        public static User GetByID(uint id, Guid guid)
        {
            if (_UserList.TryGetValue(id, out User result))
                return result;
            var type = _GuidToType[guid];
            result = (User)type.GetConstructor(Type.EmptyTypes).Invoke(null);
            result.ID = id;
            _UserList.Add(id, result);
            return result;
        }

        public enum GenderType
        {
            Male,
            Female
        }

        private string _UserID;
        public string UserID
        {
            get => (string)DataServiceFactory.DataService.GetValue(this, typeof(User).GetProperty(nameof(UserID)));
            set => DataServiceFactory.DataService.SetValue(this, typeof(User).GetProperty(nameof(UserID)), value);
        }

        private string _Name;
        public string Name
        {
            get => (string)DataServiceFactory.DataService.GetValue(this, typeof(User).GetProperty(nameof(Name)));
            set => DataServiceFactory.DataService.SetValue(this, typeof(User).GetProperty(nameof(Name)), value);
        }
        private GenderType? _Gender;
        public GenderType? Gender
        {
            get => (GenderType)DataServiceFactory.DataService.GetValue(this, typeof(User).GetProperty(nameof(Gender)));
            set => DataServiceFactory.DataService.SetValue(this, typeof(User).GetProperty(nameof(Gender)), value);
        }
    }
}
