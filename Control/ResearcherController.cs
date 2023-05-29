using AssigmentSpecification.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AssigmentSpecification.Database;
namespace AssigmentSpecification.Control
{
    public class ResearcherController
    {
        public static List<Researcher> listControllerResearcher { get;  set; }
        public static List<Researcher> filteredResearcherController { get; set; }
        public static Researcher CurrentResearcher { get;  set; }
        
        public static List<Researcher> LoadResearcher()
        {
            listControllerResearcher = ERDAdapter.FetchBasicResearcherDetails();
            return filteredResearcherController = listControllerResearcher;
        }
        
        public static List<Researcher> FilterBy(EmploymentLevel level)
        {
            List<Researcher> researchers = listControllerResearcher;

            if (level != EmploymentLevel.Other)
            {
                researchers = researchers.Where(entry => entry.Job == level).ToList();
            }

            return researchers.OrderBy(x => x.FamilyName).ToList();
        }


        public static List<Researcher> FilterByName(string ResearcherName)
        {
            List<Researcher> researchers = listControllerResearcher;

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
        public static List<Researcher> LoadSupervision(Researcher rStaff)
        {
            List<Researcher> supervisees = listControllerResearcher
                .Where(researcher => researcher is Student && (researcher as Student).SupervisorName == rStaff.Id)
                .ToList();

            return supervisees;
        }
        public static List<Researcher> FilterResearcherByNameAndLevel(EmploymentLevel level, string name)
        {
            // Convert the name to uppercase for case-insensitive comparison
            string upperCaseName = name.ToUpper();

            var filtered = listControllerResearcher.Where(researcher =>
                researcher.Job == level &&
                (researcher.GivenName.ToUpper().Contains(upperCaseName) || researcher.FamilyName.ToUpper().Contains(upperCaseName))
            );

            filteredResearcherController = new List<Researcher>(filtered);
            return filteredResearcherController;
        }
        public static string searchResearcher(int id)
        {
            string name = "";
            foreach (Researcher researcher in listControllerResearcher)
            {
                if (researcher.Id == id)
                {
                    name = $"{researcher.GivenName}, {researcher.FamilyName} ({researcher.Title})";
                    break;
                }
            }
            return name;
        }

        public static List<Researcher> GetSupervisionList()
        {
            if (CurrentResearcher is Staff staff)
            {
                return staff.Supervisions;
            }

            return new List<Researcher>();
        }
        public static void LoadResearcherDetail(int researcherId)
        {
            Researcher researcher = listControllerResearcher.FirstOrDefault(r => r.Id == researcherId);
            if (researcher != null)
            {
                researcher = ERDAdapter.CompleteResearcherDetails(researcher);
                researcher.PublicationList = PublicationController.LoadPublicationList(researcher);
                researcher.PublicationCount = researcher.PublicationList.Count;

                if (researcher is Staff staff)
                {
                    staff.Supervisions = LoadSupervision(staff);
                }

                CurrentResearcher = researcher;
            }
        }
        public static List<(int, int)> CumulativeCount()
        {
            List<(int, int)> cumulativeList = new List<(int, int)>();

            List<Publication> filteredYearPub = new List<Publication>(CurrentResearcher.PublicationList);
            filteredYearPub.Sort((pub1, pub2) => pub1.Year.CompareTo(pub2.Year));

            int currentYear = 0;
            int publicationCount = 0;

            foreach (Publication publication in filteredYearPub)
            {
                if (publication.Year > currentYear)
                {
                    currentYear = publication.Year;
                    cumulativeList.Add((currentYear, 1));
                    publicationCount = 1;
                }
                else
                {
                    publicationCount++;
                    cumulativeList[cumulativeList.Count - 1] = (currentYear, publicationCount);
                }
            }

            // Print cumulative counts
            foreach ((int year, int count) in cumulativeList)
            {
                Console.WriteLine("Year: " + year + "\tPublication Count: " + count);
            }

            return cumulativeList;
        }




    }
}

