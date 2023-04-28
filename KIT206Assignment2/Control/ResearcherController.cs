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
        }

        static List<Researcher> FilterBy(List<Researcher> researcher, EmploymentLevel level)
        {
            return ;
        }

        static List<Researcher> FilterByName(List<Researcher> researcher, string name)
        {
            return ;
        }

        public void LoadRearcherDetails()
        {
            PublicationController p;
            Researcher r;

            p.loadPublicationsFor(r);
        }

        public void Display()
        {

        }
    }
}
