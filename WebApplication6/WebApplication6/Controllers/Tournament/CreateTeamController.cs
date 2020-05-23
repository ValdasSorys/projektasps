using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication6.Models;

namespace WebApplication6.Controllers
{
    public class CreateTeamController : Controller
    {
        public ActionResult openCreateTeamView()
        {
            if (playerCanCreate() == true)
                return View("~/Views/Tournament/CreateTeam.cshtml");
            else
            {
                var Teams = Team.getTeams();
                ViewBag.isError = true;
                ViewBag.error = "Cannot create a new team while already in one";
                return View("~/Views/Tournament/TeamList.cshtml", Teams);
            }
        }

        [HttpPost]
        public ActionResult openCreateTeamView(Team team)
        {
            if (playerCanCreate() == true)
            {
                if (createTeam(team))
                {
                    return RedirectToAction("openCreateTeamView");
                }
                else
                {
                    return View("~/Views/Tournament/CreateTeam.cshtml", team);
                }
            }
            else
            {
                var Teams = Team.getTeams();

                return View("~/Views/Tournament/TeamList.cshtml", Teams);
            }
        }

        private bool createTeam(Team team)
        {
            //TODO VALIDATION
            if (validateTeam(team))
            {
                Team.create(team);

                return true;
            }
            else
            {
                return false;
            }
        }

        //VALIDATE NEWS ARTICLE
        private bool validateTeam(Team team)
        {
            //If article is being created
            if (team.Id == 0 && team.Name == null)
            {
                if (ModelState["name"].Errors.Count == 0 && ModelState["matchCount"].Errors.Count == 0 && ModelState["winCount"].Errors.Count == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            //If article is being edited
            return ModelState.IsValid;
        }
        public bool playerCanCreate()
        {
            return Team.canPlayerCreate();
        }

    }
}