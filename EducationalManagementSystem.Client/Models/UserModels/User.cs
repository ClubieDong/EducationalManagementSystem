using EducationalManagementSystem.Client.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace EducationalManagementSystem.Client.Models.UserModels
{
    [SuppressMessage("样式", "IDE0044:添加只读修饰符", Justification = "<挂起>")]
    [SuppressMessage("代码质量", "IDE0051:删除未使用的私有成员", Justification = "<挂起>")]
    public class User : ObjectWithID
    {
        private static readonly Dictionary<uint, User> _UserList = new Dictionary<uint, User>();
        public static User GetByID(uint id, Type type)
        {
            if (_UserList.TryGetValue(id, out User result))
                return result;
            result = (User)type.GetConstructor(Type.EmptyTypes).Invoke(null);
            result.ID = id;
            _UserList.Add(id, result);
            return result;
        }
        public static User GetByID(uint id) => GetByID(id, typeof(User));

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
    }
}
