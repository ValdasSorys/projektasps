using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication6.Models;

namespace WebApplication6.Controllers
{
    public class NewsController : Controller
    {
        public ActionResult openNewsList()
        {
            var News = NewsPost.select();

            return View("~/Views/Forum and news/NewsList.cshtml", News);
        }


    }
}