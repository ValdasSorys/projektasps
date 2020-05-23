/**
 * @(#) MatchMessage.cs
 */
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;


namespace WebApplication6.Models
{
	public class MatchMessage
	{
		string text;
		
		CSGOMatch match;

		private static string ConnectionString = ConfigurationManager.ConnectionStrings["mySQLConnection"].ConnectionString;

		public void addMessage( int playerid, int matchid, string text )
		{
			var Connection = new MySqlConnection(ConnectionString);
			Connection.Open();
			string SQLStatement = "INSERT INTO match_message ( text, csgomatch_id) VALUES ('" + playerid + ": " + text + "', " + matchid + ")";
			var Command = new MySqlCommand(SQLStatement, Connection);
			Command.ExecuteNonQuery();
		}

		public string[] getMessages(int matchid)
        {
			List<string> messagesToReturn = new List<string>();
			var Connection = new MySqlConnection(ConnectionString);
			Connection.Open();
			string SQLStatement = "SELECT text FROM  match_message WHERE csgomatch_id = " + matchid;
			var Command = new MySqlCommand(SQLStatement, Connection);
			MySqlDataReader Reader = Command.ExecuteReader();

			if (Reader.HasRows)
			{
				while(Reader.Read())
                {
					string text = Reader.GetString(0);
					messagesToReturn.Add(text);
                }
			}
			Reader.Close();
			Connection.Close();
			return messagesToReturn.ToArray();
        }
		
	}
	
}
