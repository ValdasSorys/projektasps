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

        private bool validateNewsArticle(NewsPost NP) 
        {
            return ModelState.IsValid;
        }

        public ActionResult openAdministrationNewsList()
        {
            var NewsPostList = NewsPost.select();

            return View("~/Views/Administration/NewsList.cshtml", NewsPostList);
        }

        public ActionResult deleteNewsArticle(int id)
        {
            NewsPost.delete(id);

            return RedirectToAction("openAdministrationNewsList");
        }
    }
}