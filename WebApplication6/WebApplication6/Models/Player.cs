/**
 * @(#) Player.cs
 */

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace WebApplication6.Models
{
	public class Player : ISUser
	{
		private static string ConnectionString = ConfigurationManager.ConnectionStrings["mySQLConnection"].ConnectionString;

		public int id;

		public CSGOMatchPlayer[] csgomatchplayer;

		public int matchCount;

		public int winCount;

		public double rating;

		//Request request;

		public bool inQueue;

		public string region;

		public DateTime inQueueSince;

		public Player()
		{

		}
		//TeamMember teammember;
		public Player(int _id, int _matchCount, int _winCount, double _rating, bool _inQueue, string _region, DateTime _inQueueSince)
		{
			id = _id;
			matchCount = _matchCount;
			winCount = _winCount;
			rating = _rating;
			inQueue = _inQueue;
			region = _region;
			inQueueSince = _inQueueSince;

		}


		public void getTopPlayers()
		{

		}

		public static List<Player> getPlayersOfTeam(int tournamentId)
		{
			var Connection = new MySqlConnection(ConnectionString);
			Connection.Open();
			string SQLStatement = "SELECT team_id FROM team_member where player_id = " + System.Web.HttpContext.Current.Session["id"].ToString();
			var Command = new MySqlCommand(SQLStatement, Connection);
			MySqlDataReader Reader = Command.ExecuteReader();
			int team;
			Reader.Read();
			team = Reader.GetInt32(0);
			Reader.Close();

            SQLStatement = "SELECT player.id, player.matchCount, player.winCount, player.rating, player.inQueue, player.region, player.inQueuesince " +
				"FROM player INNER JOIN team_member ON team_member.player_id = player.id INNER JOIN team ON team.id = team_member.team_id WHERE " +
				"team.id = " + System.Web.HttpContext.Current.Session["id"].ToString();
            Command = new MySqlCommand(SQLStatement, Connection);
            Reader = Command.ExecuteReader();

            List<Player> players = new List<Player>();

            if (Reader.HasRows)
            {
                while (Reader.Read())
                {
                    int id = Reader.GetInt32(0);
                    int matchCount = Reader.GetInt32(1);
                    int winCount = Reader.GetInt32(2);
                    double rating = Reader.GetDouble(3);
                    string region = Reader.GetString(5);
                    DateTime inQueueSince = Reader.GetDateTime(6);
                    players.Add(new Player(id, matchCount, winCount, rating, true, region, inQueueSince));
                }
            }

            Reader.Close();
            Connection.Close();
			return players;
		}

		public bool checkIsSearching(int id)
		{
			bool isSearching = false;
			var Connection = new MySqlConnection(ConnectionString);
			Connection.Open();
			string SQLStatement = "SELECT inQueue from player WHERE id = " + id;
			var Command = new MySqlCommand(SQLStatement, Connection);
			MySqlDataReader Reader = Command.ExecuteReader();
			if (Reader.HasRows)
			{
				Reader.Read();
				{
					if (Reader.GetInt32(0) == 0)
                    {
						isSearching = false;
                    }
					else
                    {
						isSearching = true;
                    }
				}
			}
			Reader.Close();
			Connection.Close();
			return isSearching;
		}

		public void addToQueue(int playerid)
		{
			var Connection = new MySqlConnection(ConnectionString);
			Connection.Open();
			string SQLStatement = "UPDATE player SET inQueue = 1, inQueueSince = NOW() WHERE id = " + playerid;
			var Command = new MySqlCommand(SQLStatement, Connection);
			Command.ExecuteNonQuery();
			Connection.Close();
		}

		public void removeFromQueue(int[] playerIds)
		{
			var Connection = new MySqlConnection(ConnectionString);
			Connection.Open();
			string playerIdString = "";
			if (playerIds.Length == 10)
			{
				playerIdString = "(" + playerIds[0] + "," + playerIds[1] + "," + playerIds[2] + "," + playerIds[3] + "," + playerIds[4] +
					"," + playerIds[5] + "," + playerIds[6] + "," + playerIds[7] + "," + playerIds[8] + "," + playerIds[9] + ")";
			}
			else
            {
				playerIdString = "(" + playerIds[0] + ")";
            }

			string SQLStatement = "UPDATE player SET inQueue = 0 WHERE id IN " + playerIdString;
			var Command = new MySqlCommand(SQLStatement, Connection);
			Command.ExecuteNonQuery();
			Connection.Close();
		}

		public List<Player> getLongestSearchingPlayer()
		{
			List<Player> players = new List<Player>();
			var Connection = new MySqlConnection(ConnectionString);
			Connection.Open();
			string SQLStatement = "SELECT * from player WHERE inQueue = 1 AND id NOT in (SELECT player_id from csgo_match_player WHERE csgomatch_id IN (SELECT id FROM csgomatch WHERE endTime is null))";
			var Command = new MySqlCommand(SQLStatement, Connection);
			MySqlDataReader Reader = Command.ExecuteReader();
			if (Reader.HasRows)
			{
				while (Reader.Read())
				{
					id = Reader.GetInt32(0);
					matchCount = Reader.GetInt32(1);
					winCount = Reader.GetInt32(2);
					rating = Reader.GetDouble(3);
					region = Reader.GetString(5);
					inQueueSince = Reader.GetDateTime(6);
					players.Add(new Player(id, matchCount, winCount, rating, true, region, inQueueSince));
				}
			}
			Reader.Close();
			Connection.Close();

			id = players[0].id;
			matchCount = players[0].matchCount;
			winCount = players[0].winCount;
			rating = players[0].rating;
			region = players[0].region;
			inQueueSince = players[0].inQueueSince;
			inQueue = true;
			players.RemoveAt(0);
			return players;
		}

		public void getSearchingPlayerByRating(List<Player> players, double[] ratings, int numberOfRatings)
		{
			double avg = 0;
			for (int i = 0; i < numberOfRatings; i++)
			{
				avg += ratings[i];
			}
			avg = avg / numberOfRatings;
			int min = 99999;
			int index = -1;
			for (int i = 0; i < players.Count; i++)
			{
				if (Math.Abs(players[i].rating - avg) < min)
				{
					index = i;
				}
			}
			id = players[index].id;
			matchCount = players[index].matchCount;
			winCount = players[index].winCount;
			rating = players[index].rating;
			region = players[index].region;
			inQueueSince = players[index].inQueueSince;
			inQueue = true;
			players.RemoveAt(index);
		}

		public bool checkPlayersDidntCancel(int[] playerIds)
		{
			bool result = false;
			var Connection = new MySqlConnection(ConnectionString);
			Connection.Open();
			string sqlStatementPart = "(";
			for (int i = 0; i < playerIds.Length; i++)
			{
				sqlStatementPart += playerIds[i] + ",";
			}
			sqlStatementPart = sqlStatementPart.Substring(0, sqlStatementPart.Length - 1) + ")";
			string SQLStatement = "SELECT Count(*) FROM player WHERE inQueue = 1 AND id IN " + sqlStatementPart;
			var Command = new MySqlCommand(SQLStatement, Connection);
			MySqlDataReader Reader = Command.ExecuteReader();
			if (Reader.HasRows)
			{
				Reader.Read();
				if (Reader.GetInt32(0) == 10)
				{
					result = true;
				}
			}
			Reader.Close();
			Connection.Close();
			return result;
		}

		public void getOtherSearchingPlayer(List<Player> players)
		{
			id = players[0].id;
			id = players[0].id;
			matchCount = players[0].matchCount;
			winCount = players[0].winCount;
			rating = players[0].rating;
			region = players[0].region;
			inQueueSince = players[0].inQueueSince;
			inQueue = true;
			players.RemoveAt(0);
		}

		public int checkSearchingCount()
		{
			int numberSearching = 0;

			var Connection = new MySqlConnection(ConnectionString);
			Connection.Open();
			string SQLStatement = "SELECT Count(*) FROM player Where inQueue = 1";
			var Command = new MySqlCommand(SQLStatement, Connection);
			MySqlDataReader Reader = Command.ExecuteReader();

			if (Reader.HasRows)
			{
				Reader.Read();
				numberSearching = Reader.GetInt32(0);
			}

			Reader.Close();
			Connection.Close();
			return numberSearching;
		}

		}

	}
