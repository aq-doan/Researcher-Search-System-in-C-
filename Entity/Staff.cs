using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBTestOnAlacritas.Entity
{
    public class Staff : Researcher
    {
        public int FundingReceived { get; set; }
        public List<Position> Positions { get; set; }
        public List<Researcher> Supervisions { get; set; }



        public float ThreeYearAverage()
        {
            int currentYear = DateTime.Now.Year;
            int totalPublications = 0;

            foreach (Publication publication in PublicationList)
            {
                if (publication.Year >= currentYear - 3)
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
        public float Q1Percentage()
        {
            int totalPublications = PublicationList.Count;
            int q1Publications = 0;

            foreach (Publication publication in PublicationList)
            {
                if (publication.Ranking == Publication.OutputRanking.Q1)
                {
                    q1Publications++;
                }
            }

            if (totalPublications == 0)
            {
                return 0;
            }

            float percentage = ((float)q1Publications / totalPublications) * 100;

            return (float)Math.Round(percentage, 2);
        }
        
        public double PerformanceByPublication()
        {
            if (DateTime.Now.Year - Tenure() < 1)
            {
                return 0;
            }

            int totalPublications = PublicationList.Count;
            double performance = (double)totalPublications / (DateTime.Now.Year - Tenure());

            return (double)Math.Round(performance, 2);
        }
        public double PerformanceByFunding()
        {
            int commencementYearCount = DateTime.Now.Year - start_job.Year + 1;
            double fundReceivePerformance = FundingReceived / (double)commencementYearCount;
            return fundReceivePerformance;
        }
    }
}


