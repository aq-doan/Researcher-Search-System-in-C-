using KIT206Assignment2.Control;
using KIT206Assignment2.Entity;
using System;
using System.Reflection;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {

            DemoSearchByLINQOnly();
            //  DemoInMemorySearch();

        }

        private static void DemoSearchByLINQOnly()
        {
            

            bool filteredPublications = PublicationController.LoadAllPublications();
            //   filteredPublications = PublicationController.SearchByResearcherUsingLINQ("Bruce Banner");

            
        }
    }
}