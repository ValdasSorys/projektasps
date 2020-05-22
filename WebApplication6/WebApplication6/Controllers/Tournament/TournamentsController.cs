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
    }
}