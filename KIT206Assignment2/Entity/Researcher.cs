using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIT206Assignment2.Entity
{
    public class Researcher
    {
        public int id { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string Title { get; set; }
        public string School { get; set; }
        public string campus { get; set; }
        public string email { get; set; }
        public string photo { get; set; }

        public List<Position> Position { get; set; }
        public List<Publication> PublicationList { get; set; }
        public DateTime start_job { get; set; }
        public Position GetCurrentJob ()
        {

        }
        public string CurrentJobTitle { get { return Position.Last().jobTitle; } }
        public DateTime CurrentJobStart()
        {

        }
        public Position GetEarliestJob()
        {

        }
        public DateTime EarliestStart { get; set; }
        
        public float Tenure{get { return (float)((DateTime.Now - start_job).TotalDays / 365);
        public int PublicationsCount()
        {

        }
    }
}
