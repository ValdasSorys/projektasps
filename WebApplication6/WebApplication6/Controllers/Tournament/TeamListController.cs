using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication6.Models;

namespace WebApplication6.Controllers
{
    public class TeamListController : Controller
    {
        public ActionResult openTeamList()
        {
            var Teams = Team.select();

            return View("~/Views/Tournament/TeamList.cshtml", Teams);
        }
    }
}