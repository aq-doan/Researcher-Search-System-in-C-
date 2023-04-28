using KIT206Assignment2.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KIT206Assignment2.Control
{
    public class ResearcherController
    {
        public void LoadResearcher()
        {
            Display();
        }

        static List<Researcher> FilterBy(List<Researcher> researcher, EmploymentLevel level)
        {
            return null;
        }

        static List<Researcher> FilterByName(List<Researcher> researcher, string name)
        {

            return null;
        }

        public void LoadRearcherDetails()
        {
            PublicationController p = new PublicationController();
            Researcher r = new Researcher();

            p.loadPublicationsFor(r);
        }

        public string Display()
        {
            return null;
        }
    }
}
