using EducationalManagementSystem.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalManagementSystem.Client.Models
{
    public abstract class ObjectWithID
    {
        public uint? ID { get; private set; }
    }
}
