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

        

        public float ThreeYearAverage()
        {
            int currentYear = DateTime.Now.Year;
            int totalPublications = 0;

            foreach (Publication publication in PublicationList)
            {
                if (publication.Year.Year >= currentYear - 3)
                {
                    totalPublications++;
                }
            }

            return (float)totalPublications / 3;
        }

        public float Performance()
        {
            float expectedPublications = 0;
            switch (Job)
            {
                case EmploymentLevel.A:
                    expectedPublications = 0.5F;
                    break;
                case EmploymentLevel.B:
                    expectedPublications = 1F;
                    break;
                case EmploymentLevel.C:
                    expectedPublications = 2F;
                    break;
                case EmploymentLevel.D:
                    expectedPublications = 3.2F;
                    break;
                case EmploymentLevel.E:
                    expectedPublications = 4F;
                    break;
            }

            return (float)Math.Round(ThreeYearAverage() / expectedPublications * 100, 1);
        }

    }
}


