using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIT206Assignment2.Entity
{
    enum OutputType
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
        public DateTime Available { get; set;}
   

    }
}
