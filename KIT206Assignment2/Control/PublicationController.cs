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
        public static bool LoadAllPublications()
        {
            publications = ERDAdapter.LoadAll();
            if (publications != null)
            {
                return true;
            }
            return false;
        }
        public Publication[] LoadPublicationsFor(Researcher r)
        {
            var pubsForResearcher = from pub in publications
                                    where pub.Authors.Contains(r.Name)
                                    select pub;

            return pubsForResearcher.ToArray();
        }

        //Sort by year

    }
}
