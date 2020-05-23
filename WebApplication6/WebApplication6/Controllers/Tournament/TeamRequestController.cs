using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication6.Models;

namespace WebApplication6.Controllers
{
    public class TeamRequestController : Controller
    {
        public ActionResult openTeamRequest(int id)
        {
            Team team = Team.select(id);
            List<Models.Request> requests = Models.Request.getTeamsRequests(team.Id);
            ViewBag.TeamID = team.Id;
            return View("~/Views/Tournament/TeamRequest.cshtml", requests);
        }

        public ActionResult deleteRequest(int id)
        {
            Models.Request request = Models.Request.select(id);
            Models.Request.deleteRequest(id);
            int teamID = request.Team_id;
            return RedirectToAction("openTeamRequest", "TeamRequest", new { id = teamID });
        }

        public ActionResult acceptRequest(int id)
        {
            Request request = Models.Request.select(id);
            Team team = Team.select(request.Team_id);
            if (team.getTeamMemberCount(team) < 5)
            {
                Team.addTeamMember(request);
                Models.Request.deleteRequest(id);
                int teamID = team.Id;
                return RedirectToAction("openTeamRequest", "TeamRequest", new { id = teamID });
            }
            else
            {
                int teamID = team.Id;
                ViewBag.TeamFull = true;
                return RedirectToAction("openTeamRequest", "TeamRequest", new { id = teamID });
            }

        }

    }
}