/**
 * @(#) MatchPlayController.cs
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication6.Models;
namespace WebApplication6.Controllers
{
	public class MatchPlayController : Controller
	{
		CSGOMatch csgomatch;

		MatchMessage matchmessage;

		CSGOMatchPlayer csgomatchplayer;


		public ActionResult openMatchView()
		{
			return View("~/Views/Game/MatchQueueView.cshtml");
		}

		public void playerIsPlaying(  )
		{
			
		}
		
		public void cancelSearch(  )
		{
			
		}
		
		[HttpPost]
		public ActionResult getPlayersOngoingMatch( int id )
		{
			List<int> test = new List<int>();
			test.Add(id);
			test.Add(id * 2);
			test.Add(id * 3);
			return Json(test);
		}
		
		public void checkResultAndCancel(  )
		{
			
		}
		
		public void wait(  )
		{
			
		}
		
		public void writeMessage(  )
		{
			
		}
		
		public void admitDefeat(  )
		{
			
		}
		
		public void checkMatchConcluded(  )
		{
			
		}
		
		public void removeFromQueue(  )
		{
			
		}
		
	}
	
}
