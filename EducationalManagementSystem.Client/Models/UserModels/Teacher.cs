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
        public Lecturer() { }
        public Lecturer(uint id) => ID = id;
    }

    public class Professor : Teacher
    {
        public Professor() { }
        public Professor(uint id) => ID = id;
    }
}
