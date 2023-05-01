using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIT206Assignment2.Entity
{
    public class Staff : Researcher
    {

        public List<Position> Positions { get; set; }
        public List<String> Supervisions { get; set; }

        private float PublicationRequirement
        {
            get
            {

                switch (Job)
                {
                    case EmploymentLevel.Student:
                        return 0F;
                    case EmploymentLevel.A:
                        return 0.5F;
                    case EmploymentLevel.B:
                        return 1F;
                    case EmploymentLevel.C:
                        return 2F;
                    case EmploymentLevel.D:
                        return 3.2F;
                    case EmploymentLevel.E:
                        return 4F;
                    default:
                        return 0;
                }
            }
        }

        //TO WORK ON THIS LATER

        public float ThreeYearAverage
        {
            get
            {
                int count = PublicationList.Count;



                return (float)(count / 3.0); //TODO
            }
        }

        public float Performance
        {
            get
            {
                return ThreeYearAverage / PublicationRequirement;
            }
        }

    }
}


