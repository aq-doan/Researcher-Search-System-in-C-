using DBTestOnAlacritas.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DBTestOnAlacritas.Database;
namespace DBTestOnAlacritas.Control
{
    public class ResearcherController
    {
        public static List<Researcher> Researchers { get; private set; }
        public static Researcher CurrentResearcher { get; private set; }
        /*
        public void LoadResearcher()
        {
            Researchers = ERDAdapter.FetchBasicResearcherDetails();
        }
        */
        public static List<Researcher> FilterBy(EmploymentLevel level)
        {
            List<Researcher> researchers = Researchers;

            if (level != EmploymentLevel.Other)
            {
                researchers = researchers.Where(entry => entry.Job == level).ToList();
            }

            return researchers.OrderBy(x => x.FamilyName).ToList();
        }


        public static List<Researcher> FilterByName(string ResearcherName)
        {
            List<Researcher> researchers = Researchers;

            if (!string.IsNullOrEmpty(ResearcherName))
            {
                string query = ResearcherName.ToUpper();

                researchers = researchers
                    .Where(r => r.GivenName.ToUpper().Contains(query) || r.FamilyName.ToUpper().Contains(query))
                    .OrderBy(r => r.FamilyName)
                    .ToList();
            }

            return researchers;
        }
        /*
          public static void LoadResearcherDetails(Researcher r)
          {
              return null

          }*/


    }
}

