using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using KIT206Assignment2.Entity;
using System.Threading.Tasks;
using System.Reflection;
using System.ComponentModel;


namespace KIT206Assignment2.Database
{
    public static class ERDAdapter
    {
        private const string db = "kit206";
        private const string user = "kit206";
        private const string pass = "kit206";
        private const string server = "alacritas.cis.utas.edu.au";

        private static MySqlConnection conn = null;


        private static MySqlConnection estConn()
        {
            if (conn == null)
            {
                string connectingdb = String.Format("Database={0};Data Source={1};User Id={2};Password={3}", db, server, user, pass);
                conn = new MySqlConnection(connectingdb);
            }
            return conn;
        }

        public static List<Researcher> FetchBasicResearcherDetails()
        {
            List<Researcher> basic = new List<Researcher>();
            MySqlConnection conn = estConn();
            MySqlDataReader readResearcher = null;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select id, given_name, family_name, title, level, email from researcher", conn);
                readResearcher = cmd.ExecuteReader();

                while (readResearcher.Read())
                {
                    Researcher researcher;
                    if (readResearcher.GetString("level") == "Student")
                    {
                        researcher = new Student();
                    }
                    else if (readResearcher.GetString("level") == "Staff")
                    {
                        researcher = new Staff();
                    }
                    else
                    {
                        continue;
                    }

                    researcher.Id = readResearcher.GetInt32("id");
                    researcher.GivenName = readResearcher.GetString("given_name");
                    researcher.FamilyName = readResearcher.GetString("family_name");
                    researcher.Title = readResearcher.GetString("title");

                    basic.Add(researcher);
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Error in fetchbasicResearcher: " + e);
            }
            finally
            {
                readResearcher.Close();
                    conn.Close();
                
            }
            return basic;
        }
    }
        public static T ParseEnum<T>(string value)
    {
        return (T)Enum.Parse(typeof(T), value);
    }

        
}