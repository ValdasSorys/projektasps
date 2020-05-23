using Antlr.Runtime.Tree;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto.Tls;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;

/**
 * @(#) CSGOMatchPlayer.cs
 */


namespace WebApplication6.Models
{public class CSGOMatchPlayer
	{
		private static string ConnectionString = ConfigurationManager.ConnectionStrings["mySQLConnection"].ConnectionString;
		public int teamNo;
		
		public Player player;
		
		public CSGOMatch csgomatch;
		
		public bool admittedDefeat;
		public CSGOMatchPlayer()
        {

        }
		public CSGOMatchPlayer(int _teamNo, Player _player, CSGOMatch _csgomatch, bool _admittedDefeat)
		{
			teamNo = _teamNo;
			player = _player;
			csgomatch = _csgomatch;
			admittedDefeat = _admittedDefeat;
		}

		public int admitDefeat( int playerid, int matchid  )
		{
			bool endMatch = false;
			int winnerTeam = -1;
			var Connection = new MySqlConnection(ConnectionString);
			Connection.Open();
			string SQLStatement = "UPDATE csgo_match_player SET admitted_Defeat = 1 WHERE player_id = " + playerid + " AND csgomatch_id = " + matchid;
			var Command = new MySqlCommand(SQLStatement, Connection);
			Command.ExecuteNonQuery();
			SQLStatement = "SELECT Count(*) FROM csgo_match_player WHERE csgomatch_id IN " +
				"(SELECT id FROM csgomatch WHERE endTime IS null AND id = " + matchid + ") AND teamNo IN " +
				"(SELECT teamno FROM csgo_match_player WHERE player_id = " + playerid + " ) AND admitted_Defeat = 1";
			Command = new MySqlCommand(SQLStatement, Connection);
			MySqlDataReader Reader = Command.ExecuteReader();

			if (Reader.HasRows)
			{
				Reader.Read();
				int count = Reader.GetInt32(0);
				if (count == 3)
                {
					endMatch = true;
                }
			}
			Reader.Close();
			if (endMatch == true)
			{
				SQLStatement = "SELECT teamNo FROM csgo_match_player WHERE player_id = " + playerid;
				Command = new MySqlCommand(SQLStatement, Connection);
				Reader = Command.ExecuteReader();
				if (Reader.HasRows)
				{
					Reader.Read();
					if (Reader.GetInt32(0) == 0)
                    {
						winnerTeam = 1;
					}
					else
                    {
						winnerTeam = 0;
                    }
				}
			}
			Reader.Close();
			Connection.Close();
			return winnerTeam;
		}
		
		public void addPlayer()
		{
			var Connection = new MySqlConnection(ConnectionString);
			Connection.Open();
			string SQLStatement = "INSERT INTO csgo_match_player ( teamno, admitted_Defeat, player_id, csgomatch_id) VALUES (-1, 0, " + player.id + " , " + csgomatch.id + ")";
			var Command = new MySqlCommand(SQLStatement, Connection);
			Command.ExecuteNonQuery();
			Connection.Close();
		}
		
		public void removeAllUnused(int matchId  )
		{
			var Connection = new MySqlConnection(ConnectionString);
			Connection.Open();
			string SQLStatement = "DELETE FROM csgo_match_player WHERE csgomatch_id = " + matchId;
			var Command = new MySqlCommand(SQLStatement, Connection);
			Command.ExecuteNonQuery();
			Connection.Close();
		}
		
	}
	
}
