using DBTestOnAlacritas.Database;
using DBTestOnAlacritas.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBTestOnAlacritas.Control
{
    public static class PublicationController
    {
        //

        public static Publication pub { get; set; }
        public static List<Publication> loadedList { get; set; }
        public static List<Publication> filtered { get; set; }
        
        public static void DisplayPublicationDetails()
        {
            Console.WriteLine("doi: " + pub.DOI);
            Console.WriteLine("title: " + pub.Title);
            Console.WriteLine("ranking: " + pub.Ranking.ToString());
            Console.WriteLine("authors: " + pub.Authors);
            Console.WriteLine("publication year: " + pub.Year.ToString());
            Console.WriteLine("publication type: " + pub.Type.ToString());
            Console.WriteLine("cite: " + pub.CiteAs);
            Console.WriteLine("available date: " + pub.Available.ToString());
        }
        public static List<Publication> LoadByYear(int start, int end)
        {
            var filtered = from Publication pub in loadedList
                           where pub.Year >= start && pub.Year <= end
                           select pub;

            List<Publication> filteredList = new List<Publication>(filtered);
            filteredList.Sort((pub1, pub2) => pub1.Title.CompareTo(pub2.Title));

            return filteredList;
        }

        public static List<Publication> LoadPublicationList(Researcher researcher)
        {
            List<Publication> loadedList = ERDAdapter.FetchBasicPublicationDetails(researcher);

            List<Publication> filtered = new List<Publication>(loadedList);
            filtered.Sort((pub1, pub2) => pub1.Title.CompareTo(pub2.Title));

            return new List<Publication>(filtered);
        }
        public static void LoadPubDetails(string doi)
        {
            Publication pub = loadedList.FirstOrDefault(p => p.DOI == doi);
            if (pub != null)
            {
                pub = ERDAdapter.CompletePublicationDetails(pub);
            }
        }
        public static List<Publication> reversePubList()
        {
            filtered.Reverse();

            return new List<Publication>(filtered);
        }

        public static void display()
        {
            foreach (Publication pub in filtered)

            {
                Console.WriteLine("\tTitle: " + pub.Title + " Year: " + pub.Year);
            }

        }
    }
}
