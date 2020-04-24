using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication6.Models;

namespace WebApplication6.Controllers.Administration
{
    public class AdministrationNewsController : Controller
    {
        private static string ConnectionString = ConfigurationManager.ConnectionStrings["mySQLConnection"].ConnectionString;

        //CREATE NEWS ARTICLE
        public ActionResult openAdministrationNewsCreate()
        {
            return View("~/Views/Administration/NewsCreate.cshtml");
        }

        [HttpPost]
        public ActionResult openAdministrationNewsCreate(NewsPost NP)
        {
            if (createNewsArticle(NP))
            {
                return RedirectToAction("openAdministrationNewsList");
            }
            else
            {
                return View("~/Views/Administration/NewsCreate.cshtml", NP);
            }
        }

        private bool createNewsArticle(NewsPost NP)
        {
            //TODO VALIDATION
            if (validateNewsArticle(NP))
            {
                NewsPost.create(NP);

                return true;
            }
            else
            {
                return false;
            }
        }

        //EDIT NEWS ARTICLE
        public ActionResult openAdministrationNewsEdit(int id)
        {
            NewsPost NP = NewsPost.select(id);

            return View("~/Views/Administration/NewsEdit.cshtml", NP);
        }

        [HttpPost]
        public ActionResult openAdministrationNewsEdit(NewsPost NP)
        {
            if (updateNewsArticle(NP))
            {
                return RedirectToAction("openAdministrationNewsList");
            }
            else
            {
                return View("~/Views/Administration/NewsEdit.cshtml", NP);
            }
        }

        private bool updateNewsArticle(NewsPost NP)
        {
            if (validateNewsArticle(NP))
            {
                NewsPost.update(NP);

                return true;
            }
            else
            {
                return false;
            }
        }

        //VALIDATE NEWS ARTICLE
        private bool validateNewsArticle(NewsPost NP) 
        {
            //If article is being created
            if (NP.Id == 0 && NP.WrittenBy == null)
            {
                if (ModelState["Title"].Errors.Count == 0 && ModelState["Text"].Errors.Count == 0 && ModelState["CreateDate"].Errors.Count == 0)
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

        //OPEN NEWS LIST
        public ActionResult openAdministrationNewsList()
        {
            var NewsPostList = NewsPost.select();

            return View("~/Views/Administration/NewsList.cshtml", NewsPostList);
        }

        //DELETE NEWS ARTICLE
        public ActionResult deleteNewsArticle(int id)
        {
            NewsPost.delete(id);

            return RedirectToAction("openAdministrationNewsList");
        }
    }
}