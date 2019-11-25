using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalManagementSystem.Client.Services.Exceptions
{
    public class NoUserIDException : ApplicationException { }
    public class WrongPasswordException : ApplicationException { }
    public class ReflectionException : ApplicationException { }
}
