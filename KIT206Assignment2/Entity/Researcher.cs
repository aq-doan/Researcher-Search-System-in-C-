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
        public string Campus { get; set; }
        public string Email { get; set; }
        public string Photo {get; set; }
        public EmploymentLevel Job { get; set; }

        public List<Position> Positions{ get; set; }
        public List<Publication> PublicationList { get; set; }
        public DateTime start_job { get; set; }
        public Position GetCurrentJob ()
        {
            return Positions.Last ();
        }
        public string CurrentJobTitle { get { return Positions.Last().jobTitle; } }

        public DateTime CurrentJobStart()
        {
            return Positions.Last().start;
        }
        public Position GetEarliestJob()
        {
            return Positions.First();
        }
        public DateTime EarliestStart { get; set; }
        
        public float Tenure { get { return (float)((DateTime.Now - start_job).TotalDays / 365); } }

        public int PublicationsCount()
        {
            return PublicationList.Count();
        }
    }
}
