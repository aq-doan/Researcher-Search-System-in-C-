using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KIT206Assignment2.Entity;
using MySql.Data.MySqlClient;

using System.Reflection;
using System.ComponentModel;
namespace KIT206Assignment2.Database
{
	//Note that ordinarily these would (1) be stored in a settings file and (2) have some basic encryption applied
	private const string db = "kit206";
	private const string user = "kit206";
	private const string pass = "kit206";
	private const string server = "alacritas.cis.utas.edu.au";

	private static MySqlConnection conn = null;

	public static MySqlConnection connect()
	{
		if (conn == null)
		{
			//Note: This approach is not thread-safe
			string connectionString = String.Format("Database={0};Data Source={1};User Id={2};Password={3}", db, server, user, pass);
			conn = new MySqlConnection(connectionString);
		}
		return conn;
	}
}