using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using AssigmentSpecification.Entity;
using System.Threading.Tasks;
using System.Reflection;
using System.ComponentModel;
using System.Xml;
using System.Xml.Linq;
using System.Reflection.Emit;


namespace AssigmentSpecification.Database
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
        public static Researcher CompleteResearcherDetails(Researcher rComplete)
        {

            return FetchFullResearcherDetail(rComplete.Id);
        }
        public  static List<Researcher> FetchBasicResearcherDetails()
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
            /* White box testing
            basic.Add(new Student
            {
                Id = 123456,
                GivenName = "John",
                FamilyName = "Doe",
                Title = "Mr",
                Job = EmploymentLevel.Student,
                SupervisorName = 123457
            });

            basic.Add(new Staff
            {
                Id = 123458,
                GivenName = "Jane",
                FamilyName = "Smith",
                Title = "Dr",
                Job = EmploymentLevel.A
            }); */
            return basic;
        }
        public static Researcher FetchFullResearcherDetail(int idFull)
        {
            MySqlDataReader readFull = null;
            MySqlConnection conn = estConn();
            List<Position> fullPositionList = new List<Position>();
            Researcher full = null; 
            try
            {
                conn.Open();
                //sql command to get all information
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM researcher WHERE id = ?id", conn);
                cmd.Parameters.AddWithValue("id", idFull);
                readFull = cmd.ExecuteReader();
                bool trueFalse = readFull.Read();
                if (trueFalse)
                {
                    EmploymentLevel l = !readFull.IsDBNull(11) ? ParseEnum<EmploymentLevel>(readFull.GetString(11)) : EmploymentLevel.Student;
                    if (l == EmploymentLevel.Student)
                    {
                        full = new Student {
                            SupervisorName = readFull.GetInt32(10),
                            Degree = readFull.GetString(9),
                            
                    };
                    } else
                    {
                        full = new Staff();
                    }
                    full.Id = readFull.GetInt32(0);
                    full.Type = ParseEnum<ResearcherType>(readFull.GetString(1));
                    full.GivenName = readFull.GetString(2);
                    full.FamilyName = readFull.GetString(3);
                    full.Title = readFull.GetString(4);
                    full.School = readFull.GetString(5);
                    full.Campus = readFull.GetString(6);
                    full.Email = readFull.GetString(7);
                    full.Photo = readFull.GetString(8);
                    full.Job = l;
                    full.start_job = readFull.GetDateTime(12);
                    full.EarliestStart = readFull.GetDateTime(13);
                };
                readFull.Close();
                MySqlCommand positionCommand = new MySqlCommand(
                  "SELECT * FROM position WHERE id = ?id",
                  conn
              );
                positionCommand.Parameters.AddWithValue("id", idFull);
                readFull = positionCommand.ExecuteReader();
                while (readFull.Read())
                {
                    fullPositionList.Add(
                        new Position
                        {
                            
                            level = ParseEnum<EmploymentLevel>(readFull.GetString(1).Last().ToString()),
                            start = readFull.GetDateTime(2),
                   
                            end = !readFull.IsDBNull(3) ? readFull.GetDateTime(3) : DateTime.MinValue
                        }
                    );
                }
                readFull.Close();
            } catch (MySqlException e) { 

                Console.WriteLine("Error in fetchfullReasearcher " + e);

            }
            finally
            {
                readFull.Close();
                conn.Close();

            }
            return full;
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
                    pub.Year = pubReader.GetInt32("year");
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
        public static int FetchPublicationCounts(DateTime from, DateTime to)
        {
            int publicationCount = 0;
            MySqlConnection conn = estConn();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM publication WHERE year >= @from AND year <= @to", conn);
                cmd.Parameters.AddWithValue("@from", from.Year);
                cmd.Parameters.AddWithValue("@to", to.Year);
                publicationCount = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Error in FetchPublicationCounts: " + e);
            }
            finally
            {
                conn.Close();
            }
            return publicationCount;
        }
    }
}
