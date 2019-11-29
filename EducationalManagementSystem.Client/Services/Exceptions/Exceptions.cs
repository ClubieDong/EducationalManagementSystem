using EducationalManagementSystem.Client.Models.CourseModels;
using System;

namespace EducationalManagementSystem.Client.Services.Exceptions
{
    public class NoUserIDException : ApplicationException { }
    public class WrongPasswordException : ApplicationException { }
    public class ReflectionException : ApplicationException { }
    public class IDDuplicatedException : ApplicationException { }
    public class ActivityOverlapException : ApplicationException
    {
        public Activity OverlappedActivity1 { get; set; }
        public Activity OverlappedActivity2 { get; set; }
    }
}
