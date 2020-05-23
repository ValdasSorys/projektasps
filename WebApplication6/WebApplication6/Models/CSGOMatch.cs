using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Text.RegularExpressions;
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

		public DateTime startTime;

		public DateTime endTime;
		
		public MatchMessage matchmessage;
		
		public void getPlayersMatches(  )
		{
			
		}
		
		public void getMatchInfo()
		{
			var Connection = new MySqlConnection(ConnectionString);
			Connection.Open();

			string SQLStatement = "SELECT * from csgo_match_player Where csgomatch_id = " + id;
			var Command = new MySqlCommand(SQLStatement, Connection);
			MySqlDataReader Reader = Command.ExecuteReader();
			List<CSGOMatchPlayer> tempList = new List<CSGOMatchPlayer>();
			if (Reader.HasRows)
			{
				while (Reader.Read())
                {
					Player tempPlayer = new Player(Reader.GetInt32(2), 0, 0, 0, false, "", new DateTime(0));
					CSGOMatchPlayer temp = new CSGOMatchPlayer(Reader.GetInt32(0), tempPlayer, this, false);
					tempList.Add(temp);
                }				
			}
			Reader.Close();

			Connection.Close();
			player = tempList.ToArray();
		}
		
		public void getOngoingMatches(int playerid )
		{
			id = 0;
			var Connection = new MySqlConnection(ConnectionString);
			Connection.Open();

			string SQLStatement = "SELECT * from csgomatch Where endTime is null " +
				"AND id in (SELECT csgomatch_id FROM csgo_match_player WHERE player_id = " + playerid + ")";
			var Command = new MySqlCommand(SQLStatement, Connection);
			MySqlDataReader Reader = Command.ExecuteReader();

			if (Reader.HasRows)
			{
				Reader.Read();
				id = Reader.GetInt32(0);
				winner = Reader.GetInt32(1);
				if (Reader.IsDBNull(2))
					startTime = new DateTime(0);
				else
					startTime = Reader.GetDateTime(2);
				if (Reader.IsDBNull(3))
					endTime = new DateTime(0);
				else
					endTime = Reader.GetDateTime(3);
			}
			Reader.Close();
			Connection.Close();
		}
		
		public void concludeMatch( int matchid, int winner )
		{
			var Connection = new MySqlConnection(ConnectionString);
			Connection.Open();

			string SQLStatement = "UPDATE csgomatch SET endTime = NOW(), winner = " + winner + " WHERE id = " + matchid;
			var Command = new MySqlCommand(SQLStatement, Connection);
			Command.ExecuteNonQuery();

			SQLStatement = "UPDATE player SET winCount = winCount + 1, " +
				"matchCount = matchCount + 1, rating = rating + 10 WHERE id IN " +
				"(SELECT player_id FROM csgo_match_player WHERE csgomatch_id = " + matchid + " AND teamNo = " + winner +")";
			Command = new MySqlCommand(SQLStatement, Connection);
			Command.ExecuteNonQuery();

			SQLStatement = "UPDATE player SET " +
				"matchCount = matchCount + 1, rating = rating - 10 WHERE id IN " +
				"(SELECT player_id FROM csgo_match_player WHERE csgomatch_id = " + matchid + " AND teamNo <> " + winner + ")";
			Command = new MySqlCommand(SQLStatement, Connection);
			Command.ExecuteNonQuery();
			Connection.Close();
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
					toReturn.endTime = new DateTime(0);
				else
					toReturn.endTime = Reader.GetDateTime(3);
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

		public bool checkMatchConcluded(int id)
        {
			bool matchConcluded = false;
			var Connection = new MySqlConnection(ConnectionString);
			Connection.Open();

			string SQLStatement = "SELECT Count(*) FROM csgomatch WHERE id = " + id + " AND endTime IS null";
			var Command = new MySqlCommand(SQLStatement, Connection);
			MySqlDataReader Reader = Command.ExecuteReader();

			if (Reader.HasRows)
			{
				Reader.Read();
				if (Reader.GetInt32(0) == 0)
                {
					matchConcluded = true;
                }				
			}
			Reader.Close();
			Connection.Close();
			return matchConcluded;
		}
		
	}
	
}
