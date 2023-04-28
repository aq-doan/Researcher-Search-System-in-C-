using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIT206Assignment2.Entity
{
    public enum EmploymentLevel
    {
        Student,
        A,
        B,
        C,
        D,
        E
    }
    public class Position
    {
        public EmploymentLevel level { get; set; }
    }
}
