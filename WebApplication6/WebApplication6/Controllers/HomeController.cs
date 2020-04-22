using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication6.Models;

namespace WebApplication6.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = new band1(1);
            return View();
        }

        public ActionResult Contact(string test1 ,string test2)
        {
            ViewBag.Message = "Your contact page.";
            ViewBag.Test1 = test1;
            ViewBag.Test2 = test2;
            return View();
        }
    }
}