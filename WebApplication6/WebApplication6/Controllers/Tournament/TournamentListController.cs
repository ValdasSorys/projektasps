﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication6.Models;

namespace WebApplication6.Controllers
{
    public class TournamentListController : Controller
    {
        // GET: TournamentList
        public ActionResult openTournamentPage()
        {
            var Tournaments = Tournament.select();

            return View("~/Views/Tournament/Main.cshtml", Tournaments);
        }
        public ActionResult openTournamentList()
        {
            var Tournaments = Tournament.select();

            return View("~/Views/Tournament/List.cshtml", Tournaments);
        }


    }
}