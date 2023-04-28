using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIT206Assignment2.Entity
{
    public class Staff : Researcher
    {
        public List<Position> positions {get; set;}
        public float requirement
        {
            get 
            {
                switch (Job)
                {
                    case EmploymentLevel.Student:
                        return 0;
                    case EmploymentLevel.A:
                        return 0.5;
                    case EmploymentLevel.B:
                        return 1;
                    case EmploymentLevel.C:
                        return 2;
                    case EmploymentLevel.D:
                        return 3.2;
                    case EmploymentLevel.E:
                        return 4;

                }
            }
        }
        public float ThreeYearAverage()
        {

        }
    }
}
