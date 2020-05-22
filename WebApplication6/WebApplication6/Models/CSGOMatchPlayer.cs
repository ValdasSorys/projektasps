using MySql.Data.MySqlClient;
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

		public void admitDefeat(  )
		{
			
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
