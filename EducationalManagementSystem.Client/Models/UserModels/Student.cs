using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalManagementSystem.Client.Models.UserModels
{
    public abstract class Student : User
    {

    }

    public class Undergraduate : Student
    {
        public Undergraduate() { }
        public Undergraduate(uint id) => ID = id;
    }

    public class Postgraduate : Student
    {
        public Postgraduate() { }
        public Postgraduate(uint id) => ID = id;
    }
}
