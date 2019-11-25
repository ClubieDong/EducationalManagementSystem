using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalManagementSystem.Client.Models.UserModels
{
    public abstract class Teacher : User
    {

    }

    public class Lecturer : Teacher
    {
    }

    public class Professor : Teacher
    {
    }
}
