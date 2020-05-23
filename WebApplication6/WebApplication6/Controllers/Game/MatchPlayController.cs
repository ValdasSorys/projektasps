/**
 * @(#) MatchPlayController.cs
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebSockets;
using WebApplication6.Models;
namespace WebApplication6.Controllers
{
	public class MatchPlayController : Controller
	{
		CSGOMatch csgomatch;

		MatchMessage matchmessage;

		CSGOMatchPlayer csgomatchplayer;

		Player player;


		public ActionResult openMatchView()
		{
			if (Session["id"] == null || Session["role"].ToString() != "user")
            {
				return RedirectToRoute("login");
			}
			int playerid = Int32.Parse(Session["id"].ToString());
			player = new Player();
			csgomatch = new CSGOMatch();
			bool isSearching = player.checkIsSearching(playerid);
			if (isSearching)
            {
				return View("~/Views/Game/MatchQueueView.cshtml");
			}
			else
            {
				csgomatch.getOngoingMatches(playerid);
				ViewBag.x = csgomatch.id;
				bool playerIsPlayingNow = playerIsPlaying();
				if (playerIsPlayingNow)
                {
					csgomatch.getMatchInfo();
					ViewBag.MatchInfo = csgomatch.id.ToString();
					string[] playerInfo1 = new string[5];
					string[] playerInfo2 = new string[5];
					int index1 = 0;
					int index2 = 0;
					for (int i = 0; i < 10; i++)
                    {
						if (csgomatch.player[i].teamNo == 0)
                        {
							playerInfo1[index1++] = csgomatch.player[i].player.id.ToString();
                        }
						else
                        {
							playerInfo2[index2++] = csgomatch.player[i].player.id.ToString();
						}
                    }
					ViewBag.PlayerInfo1 = playerInfo1;
					ViewBag.PlayerInfo2 = playerInfo2;
					return View("~/Views/Game/MatchInfoView.cshtml");
				}
				else
                {
					player.addToQueue(playerid);
					return View("~/Views/Game/MatchQueueView.cshtml");
				}
            }
		}

		public bool playerIsPlaying(  )
		{
			if (csgomatch.id != 0)
				return true;
			else
				return false;
					
		}
		
		public void cancelSearch(  )
		{

		}
		
		[HttpPost]
		public ActionResult getPlayersOngoingMatch( int id )
		{
			if (Session["id"] == null || Session["role"].ToString() != "user")
			{
				return RedirectToRoute("login");
			}
			csgomatch = new CSGOMatch();
			csgomatch.getOngoingMatches(id);
			csgomatch.getMatchInfo();
			if (csgomatch.id != 0)
			{
				return Json("1");
			}
			else
            {
				return Json("0");
			}
		}
		
		[HttpPost]
		public ActionResult writeMessage( int playerid_, int matchid_, string text )
		{
			if (Session["id"] == null || Session["role"].ToString() != "user")
			{
				return RedirectToRoute("login");
			}
			if (playerid_ != Int32.Parse(Session["id"].ToString()))
			{
				return RedirectToRoute("login");
			}
			matchmessage = new MatchMessage();
			matchmessage.addMessage(playerid_, matchid_, text);
			return Json("1");
		}
		[HttpPost]
		public ActionResult admitDefeat( int playerid_, int matchid_ )
		{
			if (Session["id"] == null || Session["role"].ToString() != "user")
			{
				return RedirectToRoute("login");
			}
			if (playerid_ != Int32.Parse(Session["id"].ToString()))
            {
				return RedirectToRoute("login");
			}
			csgomatchplayer = new CSGOMatchPlayer();
			int winnerTeam = csgomatchplayer.admitDefeat(playerid_, matchid_);
			csgomatch = new CSGOMatch();
			if (winnerTeam != -1)
            {
				csgomatch.concludeMatch(matchid_, winnerTeam);
            }
			return Json("Success");
		}
		[HttpPost]
		public ActionResult checkMatchConcluded( int id )
		{
			if (Session["id"] == null || Session["role"].ToString() != "user")
			{
				return RedirectToRoute("login");
			}
			csgomatch = new CSGOMatch();
			bool matchConcluded = csgomatch.checkMatchConcluded(id);
			if (matchConcluded)
            {
				return Json("1");
            }
			return Json("2");
		}
		
		[HttpPost]
		public ActionResult removeFromQueue( int id)
		{
			if (Session["id"] == null || Session["role"].ToString() != "user")
			{
				return RedirectToRoute("login");
			}
			if (id != Int32.Parse(Session["id"].ToString()))
			{
				return RedirectToRoute("login");
			}
			player = new Player();
			player.removeFromQueue(new int[] { id });
			return Json("");
		}

		[HttpPost]
		public ActionResult getMessagesString(int id)
		{
			matchmessage = new MatchMessage();
			string[] text = matchmessage.getMessages(id);
			string allMessages = "";
			for (int i = 0; i < text.Length; i++)
            {
				if (i != text.Length -1)
                {
					allMessages += text[i] + "\n";
                }
				else
                {
					allMessages += text[i];
                }
            }
			return Json(allMessages);
        }
		
	}
	
}
