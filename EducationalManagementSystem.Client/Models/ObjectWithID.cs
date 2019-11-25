using EducationalManagementSystem.Client.Models.HierarchyModels;
using EducationalManagementSystem.Client.Models.UserModels;
using System;
using System.Collections.Generic;

namespace EducationalManagementSystem.Client.Models
{
    public abstract class ObjectWithID
    {
        protected static readonly Dictionary<Guid, Type> _GuidToType= new Dictionary<Guid, Type>()
        {
            { typeof(ObjectWithID).GUID, typeof(ObjectWithID) },

            { typeof(User).GUID, typeof(User) },
            { typeof(Student).GUID, typeof(Student) },
            { typeof(Teacher).GUID, typeof(Teacher) },
            { typeof(Administrator).GUID, typeof(Administrator) },
            { typeof(Undergraduate).GUID, typeof(Undergraduate) },
            { typeof(Postgraduate).GUID, typeof(Postgraduate) },
            { typeof(Lecturer).GUID, typeof(Lecturer) },
            { typeof(Professor).GUID, typeof(Professor) },
            { typeof(CollegeAdministrator).GUID, typeof(CollegeAdministrator) },
            { typeof(UniversityAdministrator).GUID, typeof(UniversityAdministrator) },

            { typeof(College).GUID, typeof(College) },
            { typeof(Major).GUID, typeof(Major) },
        };

        public uint? ID { get; set; }

        public static ObjectWithID GetByID(uint id, Guid guid)
        {
            var type = _GuidToType[guid];
            Type baseType = type;
            while (baseType.BaseType != typeof(ObjectWithID))
                baseType = baseType.BaseType;
            var dict = baseType.GetProperty($"{baseType.Name}List").GetValue(null);
            var dictType = dict.GetType();
            var containsKeyFunc = dictType.GetMethod("ContainsKey");
            if ((bool)containsKeyFunc.Invoke(dict, new object[] { id }))
            {
                var getItemFunc = dictType.GetMethod("get_Item");
                return (ObjectWithID)getItemFunc.Invoke(dict, new object[] { id });
            }
            var result = (ObjectWithID)type.GetConstructor(Type.EmptyTypes).Invoke(null);
            var addFunc = dictType.GetMethod("Add");
            result.ID = id;
            addFunc.Invoke(dict, new object[] { id, result });
            return result;
        }
    }
}
