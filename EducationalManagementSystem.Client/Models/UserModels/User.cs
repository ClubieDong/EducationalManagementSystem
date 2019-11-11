using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalManagementSystem.Client.Models.UserModels
{
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
    }
}
