using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication6.Models;

namespace WebApplication6.Controllers
{
    public class TeamInfoController : Controller
    {
        public ActionResult openTeamInfo(int id)
        {
            var Teams = Team.select(id);

            return View("~/Views/Tournament/TeamInfo.cshtml", Teams);
        }
    }
}