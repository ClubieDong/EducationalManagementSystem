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
        };

        public uint? ID { get; protected set; }
    }
}
