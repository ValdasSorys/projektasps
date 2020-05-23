using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication6.Models;



namespace WebApplication6.Controllers
{
    public class TournamentsController : Controller
    {
        // GET: Tournament
        public ActionResult openTournamentInfo(int id)
        {
            var Tournaments = Tournament.select(id);

            return View("~/Views/Tournament/Info.cshtml", Tournaments);
        }
        public bool checkTimeToStart(DateTime date)
        {
            TimeSpan interval = new TimeSpan(30, 15, 0);
            if(date > DateTime.Now+interval)
            {
                return true;
            }
            return false;
        }
        public ActionResult registerTournament(int id)
        {
            var tournament = Tournament.select(id);
            bool error = true;

            if (checkTimeToStart(tournament.StartDate))
            {
                Tournament.update(id, System.Web.HttpContext.Current.Session["id"].ToString());                
            }
            else
            {
                error = false;
            }
            ViewBag.Error = error;
            var Tournaments = Tournament.select(id);
            return View("~/Views/Tournament/Info.cshtml", Tournaments);
        }
        public ActionResult openActiveTournaments()
        {
            var tournaments = Tournament.selectActive();

            return View("~/Views/Tournament/ListActive.cshtml", tournaments);
        }
        public ActionResult openTournamentLobby(int id)
        {
            var players = Player.getPlayersOfTeam(id);

            return View("~/Views/Tournament/Lobby.cshtml", players);
        }
    }
}