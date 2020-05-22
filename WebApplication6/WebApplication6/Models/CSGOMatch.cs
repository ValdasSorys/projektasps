using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
/**
* @(#) CSGOMatch.cs
*/
namespace WebApplication6.Models
{public class CSGOMatch
	{

		private static string ConnectionString = ConfigurationManager.ConnectionStrings["mySQLConnection"].ConnectionString;

		public int id;

		public CSGOMatchPlayer[] player;
		
		public int winner = -1;
		
		public int state;

		public DateTime startTime;

		public DateTime endTime;
		
		public MatchMessage matchmessage;
		
		public void getPlayersMatches(  )
		{
			
		}
		
		public void getOngoingMatches(  )
		{
			
		}
		
		public void getMatchInfo(  )
		{
			
		}
		
		public void concludeMatch(  )
		{
			
		}
		
		public void getPlayersOngoingMatch(  )
		{
			
		}
		
		public void addMatch(  )
		{
			var Connection = new MySqlConnection(ConnectionString);
			Connection.Open();
			string SQLStatement = "INSERT INTO csgomatch ( ) VALUES ()";
			var Command = new MySqlCommand(SQLStatement, Connection);
			Command.ExecuteNonQuery();

			SQLStatement = "SELECT id FROM csgomatch ORDER BY ID DESC LIMIT 1";
			Command = new MySqlCommand(SQLStatement, Connection);
			MySqlDataReader Reader = Command.ExecuteReader();

			if (Reader.HasRows)
			{
				Reader.Read();
				id = Reader.GetInt32(0);
			}
			Reader.Close();
			Connection.Close();
		}
		
		public void addPlayerToMatch()
		{
			
		}
		
		public void startMatch( int[] playerIds )
		{
			var Connection = new MySqlConnection(ConnectionString+ ";Convert Zero Datetime=True");
			Connection.Open();
			string SQLStatement = "UPDATE csgomatch SET startTime = NOW() WHERE id = " + id;
			var Command = new MySqlCommand(SQLStatement, Connection);
			Command.ExecuteNonQuery();
			string team1ids = "(" + playerIds[0] + "," + playerIds[1] + "," + playerIds[2] + "," + playerIds[3] + "," + playerIds[4] + ")";
			string team2ids = "(" + playerIds[5] + "," + playerIds[6] + "," + playerIds[7] + "," + playerIds[8] + "," + playerIds[9] + ")";
			SQLStatement = "UPDATE csgo_match_player SET teamno = 0 WHERE csgomatch_id = " + id + " AND player_id IN " + team1ids;
			Command = new MySqlCommand(SQLStatement, Connection);
			Command.ExecuteNonQuery();
			SQLStatement = "UPDATE csgo_match_player SET teamno = 1 WHERE csgomatch_id = " + id + " AND player_id IN " + team2ids;
			Command = new MySqlCommand(SQLStatement, Connection);
			Command.ExecuteNonQuery();
			Connection.Close();
		}
		
		public void test ()
        {
			var Connection = new MySqlConnection(ConnectionString);
			Connection.Open();
			string SQLStatement = "SELECT * FROM  csgomatch Where id in " +
			"(SELECT csgomatch_id FROM csgo_match_player WHERE player_id = '" + 1 + "' ) ORDER BY endTime DESC LIMIT 1";
			//string SQLStatement = "SELECT csgomatch_id FROM csgo_match_player WHERE player_id = " + player_id;
			var Command = new MySqlCommand(SQLStatement, Connection);
			MySqlDataReader Reader = Command.ExecuteReader();


			if (Reader.HasRows)
			{
				Reader.Read();
			}

			Reader.Close();
			Connection.Close();
		}
		public CSGOMatch getLastPlayedMatch( int player_id )
		{
			CSGOMatch toReturn = new CSGOMatch();
			//CSGOMatch toReturn = new CSGOMatch();
			var Connection = new MySqlConnection(ConnectionString);
			Connection.Open();
			string SQLStatement = "SELECT * FROM  csgomatch Where id in " +
			"(SELECT csgomatch_id FROM csgo_match_player WHERE player_id = '" + player_id + "' ) ORDER BY endTime DESC LIMIT 1";
			//string SQLStatement = "SELECT csgomatch_id FROM csgo_match_player WHERE player_id = " + player_id;
			var Command = new MySqlCommand(SQLStatement, Connection);
			MySqlDataReader Reader = Command.ExecuteReader();

			if (Reader.HasRows)
			{
				Reader.Read();
				toReturn.id = Reader.GetInt32(0);
				toReturn.winner = Reader.GetInt32(1);
				if (Reader.IsDBNull(2))
					toReturn.startTime = new DateTime(0);
				else
					toReturn.startTime = Reader.GetDateTime(2);
				if (Reader.IsDBNull(3))
					toReturn.startTime = new DateTime(0);
				else
					toReturn.startTime = Reader.GetDateTime(3);
			}
			
			Reader.Close();
			List<CSGOMatchPlayer> playersOfMatch = new List<CSGOMatchPlayer>();
			if (toReturn.id != 0)
			{
				SQLStatement = "SELECT * FROM  csgo_match_player Where csgomatch_id = " + toReturn.id;
				Command = new MySqlCommand(SQLStatement, Connection);
				
				Reader = Command.ExecuteReader();
				if (Reader.HasRows)
				{
					while (Reader.Read())
					{
						playersOfMatch.Add(new CSGOMatchPlayer(-1, new Player(Reader.GetInt32(2), 0, 0, 0, false, "0", new DateTime(0)), null, false));
					}
				}
			}
			Reader.Close();
			Connection.Close();
			toReturn.player = playersOfMatch.ToArray();
			Connection.Close();
			return toReturn;
		}
		
		public void removeMatch(  )
		{
			var Connection = new MySqlConnection(ConnectionString);
			Connection.Open();
			string SQLStatement = "DELETE FROM csgomatch WHERE id = " + id;
			var Command = new MySqlCommand(SQLStatement, Connection);
			Command.ExecuteNonQuery();
			Connection.Close();
		}
		
	}
	
}
