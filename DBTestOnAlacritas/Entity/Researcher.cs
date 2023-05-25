using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBTestOnAlacritas.Entity
{
    public enum ResearcherType { Staff, Student, EnumCount }
    public class Researcher
    {
        public int Id { get; set; }
        public ResearcherType Type { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string Title { get; set; }
        public string School { get; set; }
        public string Campus { get; set; }
        public string Email { get; set; }
        public string Photo { get; set; }
        public EmploymentLevel Job { get; set; }

        public List<Position> Positions { get; set; }
        public List<Publication> PublicationList { get; set; }
        public DateTime start_job { get; set; }
        public Position GetCurrentJob()
        {
            return Positions.Last();
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

        public double Tenure()
        {
            return DateTime.Now.Year - start_job.Year;
        }


        public int PublicationCount { get; set; }
    }
    
}

