using MySqlX.XDevAPI.Common;
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
            var team = Team.select(id);
            var teamMembers = Team.getTeamInfo(id);
            int usersRole = getTeamRole(team);
            if (usersRole == 2)
                ViewBag.IsCaptain = true;
            ViewBag.teamMembers = teamMembers;
            return View("~/Views/Tournament/TeamInfo.cshtml", team);
        }
        
        public int getTeamRole(Team team)
        {
            int result = Team.getRole(team);
            return result;
        }

    }
}