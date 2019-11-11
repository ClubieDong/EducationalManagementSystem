using EducationalManagementSystem.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace EducationalManagementSystem.Client.Models
{
    public abstract class ObjectWithID
    {
        public uint? ID { get; protected set; }
    }
}
