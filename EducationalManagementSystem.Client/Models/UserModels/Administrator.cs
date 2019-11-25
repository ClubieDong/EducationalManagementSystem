using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalManagementSystem.Client.Models.UserModels
{
    public abstract class Administrator : User
    {
    }

    public class CollegeAdministrator : Administrator
    {
    }

    public class UniversityAdministrator : Administrator
    {
    }
}
