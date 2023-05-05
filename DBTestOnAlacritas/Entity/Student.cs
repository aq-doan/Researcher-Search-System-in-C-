using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBTestOnAlacritas.Entity
{
    public class Student : Researcher
    {
        public string Degree { get; set; }
        public string SupervisorName { get; set; }
    }
}
