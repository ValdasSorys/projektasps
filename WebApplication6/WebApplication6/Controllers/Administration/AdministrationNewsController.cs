using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication6.Models;

namespace WebApplication6.Controllers.Administration
{
    public class AdministrationNewsController : Controller
    {
        // GET: AdministrationNews
        public ActionResult openAdministrationNewsList()
        {
            var NewsPostList = NewsPost.select();

            return View("~/Views/Administration/NewsList.cshtml", NewsPostList);
        }
    }
}