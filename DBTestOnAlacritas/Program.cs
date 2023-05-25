using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using DBTestOnAlacritas.Database;
namespace DBTestOnAlacritas
{
    class Program
    {
        //Note that ordinarily these would (1) be stored in a settings file and (2) have some basic encryption applied
        private const string db = "kit206";
        private const string user = "kit206";
        private const string pass = "kit206";
        private const string server = "alacritas.cis.utas.edu.au";

        private MySqlConnection conn;


        static void Main(string[] args)
        {
            Console.WriteLine("testing has begun");

            ERDAdapter demo = new ERDAdapter();


            Console.WriteLine("Names from researcher table:");
            
            Console.WriteLine();
            Console.WriteLine("Names read into a DataSet (should be the same as above):");
     

            Console.ReadLine();
        }

        /*
         * Using the ExecuteReader method to select from a single table
         */
       
    }
}
