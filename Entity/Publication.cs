﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBTestOnAlacritas.Entity
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
        public string CiteAs { get; set; }
        public List<string> Authors { get; set; }
        public int Year { get; set; }
        public DateTime Available { get; set; }
        public OutputType Type { get; set; }
        public OutputRanking Ranking { get; set; }

        public Publication()
        {
            Authors = new List<string>();
        }
        public void AddAuthorName(string authorName)
        {
            Authors.Add(authorName);
        }
        public int AgePub
        {
            get
            {
                return Age();
            }
        }
        public int Age()
        {
            int result = DateTime.Now.Year - Year;
            return result;
        }
        public enum OutputRanking
        {
            Q1,
            Q2,
            Q3,
            Q4
        }
        public override string ToString()
        {
            return DOI + " - " + Title + " - " + Authors[0] + " et. al";
        }
    }
}