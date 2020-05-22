/**
 * @(#) MatchCreationController.cs
 */
using Microsoft.Ajax.Utilities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using WebApplication6.Models;
namespace WebApplication6.Controllers
{
	public class MatchCreationController
	{
		private static string ConnectionString = ConfigurationManager.ConnectionStrings["mySQLConnection"].ConnectionString;





		public bool checkPlayerSearchLength( DateTime dateToCompare )
		{
			TimeSpan searchedFor = player.inQueueSince - dateToCompare;
			if (Math.Abs(searchedFor.TotalMinutes) >= 2)
            {
				return true;
            }
			return false;
		}

		
		public bool checkRegion( string region )
		{
			if (player.region == region)
            {
				return true;
            }
			return false;
		}
		
		public bool checkDidntPlayTogether( CSGOMatch match, int player_id, int[] addedPlayerIds)
		{
			foreach (var x in match.player)
            {
				if (addedPlayerIds.Contains(x.player.id))
                {
					return false;
                }				
            }
			return true;
		}
		
		public bool checkMatchFull( int playerCount )
		{
			if (playerCount == 10)
            {
				return true;
            }
			return false;
			
		}
		//checkRating(ratings, numberOfRatings);
		public bool checkRating(double[] ratings, int numberOfRatings )
		{
			double ratingAvg = 0;
			for (int i = 0; i < numberOfRatings; i++)
            {
				ratingAvg += ratings[i];
            }
			ratingAvg = ratingAvg / numberOfRatings;
			if (Math.Abs(player.rating-ratingAvg) <= 20)
            {
				return true;
            }
			return false;

		}
		
		public bool checkAnyLeftPlayers( List<Player> players )
		{
			if (players.Count != 0)
			{
				return true;
			}
			return false;
		}
		
		public void initiateSearch(  )
		{
			player = new Player();
			int searchingCount = player.checkSearchingCount();


			if (searchingCount <10)
				return;
			List<Player> players = player.getLongestSearchingPlayer();
			csgomatch = new CSGOMatch();
			csgomatch.addMatch();



			csgomatchplayer = new CSGOMatchPlayer();
			csgomatchplayer.player = player;
			csgomatchplayer.csgomatch = csgomatch;
			csgomatchplayer.addPlayer();
			double[] ratings = new double[10];
			ratings[0] = player.rating;
			int numberOfRatings = 1;

			int[] addedPlayerIds = new int[10];
			addedPlayerIds[0] = player.id;
			int addedPlayersCount = 1;

			string region = player.region;
			if (!(searchingCount < 30))
			{
				for (int i = 1;  i < 10; i++)
                {
					player.getSearchingPlayerByRating(players, ratings, numberOfRatings);
					ratings[i] = player.rating;
					addedPlayerIds[i] = player.id;
					csgomatchplayer.addPlayer();
					numberOfRatings += 1;
                }
				/*var Connection = new MySqlConnection(ConnectionString);
				Connection.Open();
				string SQLStatement = "UPDATE player SET inQueue = 0 Where id = 100";
				var Command = new MySqlCommand(SQLStatement, Connection);
				Command.ExecuteNonQuery();
				Connection.Close();*/
				bool didntCancel = player.checkPlayersDidntCancel(addedPlayerIds);
				if (didntCancel == true)
                {
					csgomatch.startMatch(addedPlayerIds);
					player.removeFromQueue(addedPlayerIds);
				}
                else
                {
					csgomatchplayer.removeAllUnused(csgomatch.id);
					csgomatch.removeMatch();
				}
			}
			else
            {
				DateTime dateToCompare = DateTime.Now;
				bool playersCountPositive = true;
				bool matchNotFull = true;
				while (playersCountPositive && matchNotFull)
				{
					
					player.getOtherSearchingPlayer(players);
					
					bool searchLength = checkPlayerSearchLength(dateToCompare);

					bool ratingAppropriate = checkRating(ratings, numberOfRatings);
					
					bool playerRegion = checkRegion(region);
					
					CSGOMatch toCheckPlayedTogether = csgomatch.getLastPlayedMatch(player.id);
					

					bool playedTogether = checkDidntPlayTogether(toCheckPlayedTogether, player.id, addedPlayerIds);

					var Connection = new MySqlConnection(ConnectionString);
					Connection.Open();

					if ((ratingAppropriate && playerRegion && playedTogether) || searchLength)
                    {
						csgomatchplayer.addPlayer();
						addedPlayerIds[addedPlayersCount++] = player.id;
                    }
					playersCountPositive = checkAnyLeftPlayers(players);
					matchNotFull = !checkMatchFull(addedPlayersCount);
					
				}

				if (checkMatchFull(addedPlayersCount))
                {
					bool didntCancel = player.checkPlayersDidntCancel(addedPlayerIds);
					if (didntCancel == true)
					{
						csgomatch.startMatch(addedPlayerIds);
						player.removeFromQueue(addedPlayerIds);
					}
                    else
                    {
						csgomatchplayer.removeAllUnused(csgomatch.id);
						csgomatch.removeMatch();
					}
				}
                else
                {
					csgomatchplayer.removeAllUnused(csgomatch.id);
					csgomatch.removeMatch();
				}
            }
		}

		CSGOMatchPlayer csgomatchplayer;

		CSGOMatch csgomatch;

		Player player;

	}
	
}
