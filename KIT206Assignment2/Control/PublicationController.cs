using KIT206Assignment2.Database;
using KIT206Assignment2.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIT206Assignment2.Control
{
    public static class PublicationController
    {
        private static List<Publication> publications;
        private static List<Publication> CurrentList = new List<Publication>();
        public static bool LoadAllPublications()
        {
            publications = ERDAdapter.FetchBasicPublicationDetails();
            if (publications != null)
            {
                return true;
            }
            return false;
        }
        public static Publication[] LoadPublicationsFor(Researcher r)
        {
            var pubsForResearcher = from pub in publications
                                    where pub.Authors.Contains(r.GivenName)
                                    select pub;

            return pubsForResearcher.ToArray();
        }

        //Sort by year
        public static List<Publication> FilterByYear(int year_min, int year_max)
        {
            var list_after = from pub in publications
                           where pub.Year.Year >= year_min && pub.Year.Year <= year_max
                           select pub;
            CurrentList = new List<Publication>(list_after);
            return CurrentList;
        }

        /* public void LoadPublicationDetails(Publication publication)
        {
            Publication pub = Database.ERDAdapter.fetchFullPublicationDetails(publication);

            string DOI = pub.DOI;
            string Title = pub.Title;
            string Authors = pub.Authors;
            string PublicationYear = pub.Year.Year.ToString();
            string Type = pub.Type.ToString();
            string Ranking = pub.Ranking.ToString();
            string AvailableDate= pub.Available.ToString("dd/MM/yyyy");
            string Age= pub.Age().ToString() + " days";

            
        }*/
    }
}
