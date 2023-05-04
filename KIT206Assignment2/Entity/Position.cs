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
        E,
        Other
    }
    public class Position
    {
        public EmploymentLevel level { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public string jobTitle
        {
            get
            {
                switch (level)
                {
                    case EmploymentLevel.Student:
                        return "Student";
                    case EmploymentLevel.A:
                        return "Research Associate";
                    case EmploymentLevel.B:
                        return "Lecturer";
                    case EmploymentLevel.C:
                        return "Assistant Professor";
                    case EmploymentLevel.D:
                        return "Associate Professor";
                    case EmploymentLevel.E:
                        return "Professor";
                    default:
                        return "Unknown";
                }
            }
        }
        public string ToTitle(EmploymentLevel type)
        {
            switch (type)
            {
                case EmploymentLevel.Student:
                    return "Student";
                case EmploymentLevel.A:
                    return "Research Associate";
                case EmploymentLevel.B:
                    return "Lecturer";
                case EmploymentLevel.C:
                    return "Assistant Professor";
                case EmploymentLevel.D:
                    return "Associate Professor";
                case EmploymentLevel.E:
                    return "Professor";
                default:
                    throw new ArgumentException("Invalid employment level.");
            }
        }
    }

}
