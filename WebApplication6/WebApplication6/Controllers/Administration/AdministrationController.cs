using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication6.Controllers.Administration
{
    public class AdministrationController : Controller
    {
        // GET: Administration
        public ActionResult openAdministrationMain()
        {
            // START ROLE CHECK
            if (System.Web.HttpContext.Current.Session["role"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (System.Web.HttpContext.Current.Session["role"].ToString() != "admin")
            {
                return RedirectToAction("Index", "Home");
            }
            // END ROLE CHECK

            return View("~/Views/Administration/Main.cshtml");
        }
    }
}