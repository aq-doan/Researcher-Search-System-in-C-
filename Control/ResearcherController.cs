using AssigmentSpecification.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AssigmentSpecification.Database;
using System.Xml.Serialization;
using System.IO;

namespace AssigmentSpecification.Control
{
    [XmlRoot(ElementName = "Projects")]
    public class ListOfProject
    {
        [XmlElement("Project")]
        public List<xmlP> pList { get; set; }
    }

    public class xmlP
    {
        [XmlElement("Funding")]
        public int Funding { get; set; }

        [XmlElement("Year")]
        public int Year { get; set; }

        [XmlElement("Researchers")]
        public List<Researchers> rListXml { get; set; }
    }

    public class Researchers
    {
        [XmlElement("staff_id")]
        public List<int> xmlIDList { get; set; }
    }

    public class CumulativePair
    {
        public int Year { get; set; }
        public int Count { get; set; }
    }

    public class PerformancePair
    {
        public int Performance { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
    }
    public class ResearcherController
    {
        public static List<Researcher> listControllerResearcher { get;  set; }
        public static List<Researcher> filteredResearcherController { get; set; }
        public static Researcher CurrentResearcher { get;  set; }

        private static ListOfProject projectControllerXml;
        public static List<Researcher> LoadResearcher()
        {
            listControllerResearcher = ERDAdapter.FetchBasicResearcherDetails();
            filteredResearcherController = listControllerResearcher;
            string fundingFilePath = "../../Control/Fundings_Rankings.xml";
            string fundingFileStr = System.IO.File.ReadAllText(fundingFilePath);

            XmlSerializer serializer = new XmlSerializer(typeof(ListOfProject));

            ListOfProject projectControllerXml;

            using (TextReader reader = new StringReader(fundingFileStr))
            {
                projectControllerXml = (ListOfProject)serializer.Deserialize(reader);
            }

            return filteredResearcherController;
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

        public static List<Researcher> GetSupervisionList(Researcher s)
        {
            List<Researcher> list = new List<Researcher>();

            foreach (var researcher in listControllerResearcher)
            {
                if (researcher is Student student && student.SupervisorName == s.Id)
                {
                    list.Add(student);
                }
            }

            return list;
        }

        public static void LoadDetailR(int researcherId)
        {
            string fundingFilePath = "../../Control/Fundings_Rankings.xml";
            using (var fileStream = new FileStream(fundingFilePath, FileMode.Open))
            {
                var serializer = new XmlSerializer(typeof(ListOfProject));
                projectControllerXml = (ListOfProject)serializer.Deserialize(fileStream);
            }

            Researcher selectedResearcher = null;

            foreach (var researcher in listControllerResearcher)
            {
                if (researcherId == researcher.Id)
                {
                
                    selectedResearcher = ERDAdapter.CompleteResearcherDetails(researcher);

               
                    selectedResearcher.PublicationList = PublicationController.LoadPublicationList(selectedResearcher);

                 
                    selectedResearcher.PublicationCount = selectedResearcher.PublicationList.Count;

                
                    if (selectedResearcher is Staff staff)
                    {
                        // Assign supervisee list for the selected staff
                        staff.Supervisions = GetSupervisionList(selectedResearcher);

                        // Calculate funding sum of the selected researcher
                        int totalFunding = 0;
                        foreach (xmlP project in projectControllerXml.pList)
                        {
                            foreach (Researchers researchers in project.rListXml)
                            {
                                foreach (int staffId in researchers.xmlIDList)
                                {
                                    if (staffId == researcherId)
                                    {
                                        totalFunding += project.Funding;
                                    }
                                }
                            }
                        }
                        staff.FundingReceived = totalFunding;
                    }

                    // Set current selected researcher
                    CurrentResearcher = selectedResearcher;

                    // Target found, stop the search loop
                    break;
                }
            }
        }




        public static List<PerformancePair>[] GetPerformanceReport()
        {
            List<PerformancePair>[] performanceListGroups = new List<PerformancePair>[4];
            for (int i = 0; i < performanceListGroups.Length; i++)
            {
                performanceListGroups[i] = new List<PerformancePair>();
            }

            foreach (var researcher in listControllerResearcher)
            {
                if (researcher is Staff staff)
                {
                    LoadDetailR(researcher.Id);

                    int performance = (int)(staff.PerformanceByPublication() * 100);
                    var performancePair = new PerformancePair
                    {
                        FirstName = researcher.GivenName,
                        LastName = researcher.FamilyName,
                        Title = researcher.Title,
                        Performance = performance
                    };

                    if (performance >= 200)
                    {
                        performanceListGroups[3].Add(performancePair);
                    }
                    else if (performance >= 110)
                    {
                        performanceListGroups[2].Add(performancePair);
                    }
                    else if (performance >= 70)
                    {
                        performanceListGroups[1].Add(performancePair);
                    }
                    else
                    {
                        performanceListGroups[0].Add(performancePair);
                    }
                }
            }

            foreach (var group in performanceListGroups)
            {
                group.Sort((perf1, perf2) => perf1.Performance.CompareTo(perf2.Performance));
            }

            return performanceListGroups;
        }


        public static string GetEmailForAll()
        {
            StringBuilder mails = new StringBuilder();

            foreach (Researcher current in listControllerResearcher)
            {
                if (current is Staff staff)
                {
                    if (mails.Length > 0)
                    {
                        mails.Append(", ");
                    }
                    mails.Append(staff.Email);
                }
            }

            return mails.ToString();
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

