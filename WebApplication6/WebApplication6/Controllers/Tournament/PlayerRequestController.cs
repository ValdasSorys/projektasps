using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication6.Models;

namespace WebApplication6.Controllers
{
    public class PlayerRequestController : Controller
    {
        public ActionResult createPlayerJoinRequest(int id)
        {
            var team = Team.select(id);
            var teamMembers = Team.getTeamInfo(id);
            ViewBag.teamMembers = teamMembers;
            if (canCreatePlayerJoinRequest())
            {
                Models.Request.createRequest(team);
                ViewBag.requestSent = true;
                return View("~/Views/Tournament/TeamInfo.cshtml", team);
            }
            else
            {
                ViewBag.isError = true;
                return View("~/Views/Tournament/TeamInfo.cshtml", team);
            }

        }

        public bool canCreatePlayerJoinRequest()
        {
            return Team.canPlayerCreate();
        }

    }
}