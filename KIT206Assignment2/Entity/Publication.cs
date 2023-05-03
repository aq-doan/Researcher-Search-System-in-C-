using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIT206Assignment2.Entity
{
    public enum OutputType
    {
        Journal,
        Conference,
        Other
    }
    public class Publication
    {
        public string DOI { get; set; }
        public string Title { get; set; }
        public string Authors { get; set; }
        public DateTime Year { get; set; }
        public DateTime Available { get; set; }
        public OutputType Type { get; set; }
        public OutputRanking Ranking { get; set; }

        public Publication()
        {
            AuthorNames = new List<string>();
        }
        public int Age()
        {
            TimeSpan age = DateTime.Now - Available;
            return (int)age.TotalDays;
        }
        public enum OutputRanking
        {
            Q1,
            Q2,
            Q3,
            Q4
        }
    }
}
