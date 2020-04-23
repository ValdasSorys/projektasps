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
            return View("~/Views/Administration/Main.cshtml");
        }
    }
}