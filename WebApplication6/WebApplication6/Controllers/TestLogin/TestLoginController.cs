using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication6.Controllers.Test
{
    public class TestLoginController : Controller
    {
        // GET: TestLogin
        public ActionResult Index()
        {
            return View("~/Views/Test/View.cshtml");
        }
        [HttpPost]
        public ActionResult LoginUser(string id)
        {
            int x = 0;
            if (!Int32.TryParse(id, out x))
                return View("~/Views/Test/View.cshtml");
            int[] testUserID = {1, 2, 3, 4, 5};
            string[] testUserRole = { "admin", "admin", "player", "player", "player"};
            if (testUserID.Contains(x))
            {
                Session["id"] = testUserID[Array.IndexOf(testUserID, x)];
                Session["role"] = testUserRole[Array.IndexOf(testUserID, x)];
                if (Session["role"].ToString() == "admin")
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("IndexGamer", "Home");
                }
            }
            else 
            { 
                return View("~/Views/Test/View.cshtml");
            }            
        }

        public ActionResult LogoutUser()
        {
            Session["id"] = null;
            Session["role"] = null;
            return View("~/Views/Test/View.cshtml");
        }
    }
}