using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebSockets;
using WebApplication6.Models;
namespace WebApplication6.Controllers
{
	public class MatchHistoryController : Controller
	{
		CSGOMatch csgomatch;
		public ActionResult openMatchHistory(  )
		{
			if (Session["id"] == null || Session["role"].ToString() != "user")
			{
				return RedirectToRoute("login");
			}
			int playerid = Int32.Parse(Session["id"].ToString());
			csgomatch = new CSGOMatch();
			List<CSGOMatch> matchhistory = csgomatch.getPlayersMatches(playerid);
			string[] historyTable = new string[matchhistory.Count];
			for (int i = 0; i < matchhistory.Count; i++)
            {
				historyTable[i] = "<tr>";
				historyTable[i] += "<td>" + matchhistory[i].id + "</td>";
				if (matchhistory[i].winner == 1)
                {
					historyTable[i] += "<td>Won</td>";
				}
				else
				{
					historyTable[i] += "<td>Lost</td>";
				}
				historyTable[i] += "<td>" + matchhistory[i].startTime.ToString() + "</td>";
				historyTable[i] += "<td>" + matchhistory[i].endTime.ToString() + "</td>";
				historyTable[i] += "</tr>";
			}
			ViewBag.historyTable = historyTable;
			return View("~/Views/Game/MatchHistoryView.cshtml");
		}
		
	}
	
}
