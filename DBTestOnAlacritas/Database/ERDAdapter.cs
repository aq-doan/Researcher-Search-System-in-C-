﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using DBTestOnAlacritas.Entity;
using System.Threading.Tasks;
using System.Reflection;
using System.ComponentModel;
using System.Xml;
using System.Xml.Linq;


namespace DBTestOnAlacritas.Database
{
    public  class ERDAdapter
    {
        private const string db = "kit206";
        private const string user = "kit206";
        private const string pass = "kit206";
        private const string server = "alacritas.cis.utas.edu.au";

        private static MySqlConnection conn = null;


        private static MySqlConnection estConn()
        {


            string connectingdb = String.Format("Database={0};Data Source={1};User Id={2};Password={3}", db, server, user, pass);
            conn = new MySqlConnection(connectingdb);

            return conn;
        }

        public  List<Researcher> FetchBasicResearcherDetails()
        {
            List<Researcher> basic = new List<Researcher>();
            MySqlConnection conn = estConn();
            MySqlDataReader readResearcher = null;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select id, given_name, family_name, title, level, supervisor_id from researcher", conn);
                readResearcher = cmd.ExecuteReader();

                while (readResearcher.Read())
                {
                    EmploymentLevel lvl = !readResearcher.IsDBNull(4) ? ParseEnum<EmploymentLevel>(readResearcher.GetString(4)) : EmploymentLevel.Student;
                    Boolean trueFalse = (lvl == EmploymentLevel.Student);
                    if (trueFalse)
                    {
                        basic.Add(new Student
                        {
                            Id = readResearcher.GetInt32("id"),
                            GivenName = readResearcher.GetString("given_name"),
                            FamilyName = readResearcher.GetString("family_name"),
                            Title = readResearcher.GetString("title"),
                            Job = lvl,
                            SupervisorName = readResearcher.GetInt32("supervisor_id")

                        }
                       ); 

                    } else
                    {
                        basic.Add(new Staff
                        {
                            Id = readResearcher.GetInt32("id"),
                            GivenName = readResearcher.GetString("given_name"),
                            FamilyName = readResearcher.GetString("family_name"),
                            Title = readResearcher.GetString("title"),
                            Job = lvl,
                            
                        }
                        );
                    }
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
        public static Researcher FetchFullResearchDetail(int id)
        {
            Researcher full = null; 
        }
        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value);
        }
        public static List<Publication> FetchBasicPublicationDetails(Researcher r)
        {
            MySqlConnection conn = estConn();
            MySqlDataReader pubReader = null;
            List<Publication> fetchBasic = new List<Publication>();

            try
            {
                conn.Open();

                MySqlCommand sqlCommand = new MySqlCommand("select pub.doi, title, year from publication as pub, " +
                                                     "researcher_publication as respub where pub.doi=respub.doi " +
                                                     "and researcher_id=@id", conn);
                sqlCommand.Parameters.AddWithValue("@id", r.Id);

                pubReader = sqlCommand.ExecuteReader();

                while (pubReader.Read())
                {
                    Publication pub = new Publication();
                    pub.Title = pubReader.GetString("title");
                    pub.Year = pubReader.GetDateTime("year");
                    pub.DOI = pubReader.GetString("doi");
                    fetchBasic.Add(pub);
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Error in FetchBasicPublicationDetails: " + e);
            }
            finally
            {
                if (pubReader != null)
                {
                    pubReader.Close();
                }
                conn.Close();
            }

            return fetchBasic;
        }
        public static Publication CompletePublicationDetails(Publication completePub)
        {
            MySqlConnection conn = estConn();
            MySqlDataReader completePubReader = null;
            try
            {
                conn.Open();


                MySqlCommand completeCommand = new MySqlCommand("select authors, type, cite_as, available " +
                                                    "from publication where doi=?doi", conn);
                completeCommand.Parameters.AddWithValue("doi", completePub.DOI);


                completePubReader = completeCommand.ExecuteReader();
                if (completePubReader.Read())
                {
                    completePub.CiteAs = completePubReader.GetString(2);
                    completePub.Available = completePubReader.GetDateTime(3);
                    // completePub.Authors = completePubReader.GetString(0);
                    completePub.Type = ParseEnum<OutputType>(completePubReader.GetString(1));

                }
                Console.WriteLine(completePub);
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Error connecting to database: " + e);
            }
            finally
            {
                completePubReader.Close();
                conn.Close();
            }

            return completePub;
        }
        //missingfetchfullresearcher
        //missingcompleteResearcherDetail
        //fetchPublicationcount
    }
}
