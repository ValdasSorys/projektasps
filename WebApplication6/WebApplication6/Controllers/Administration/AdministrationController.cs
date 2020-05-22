using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication6.Controllers.Administration
{
    public class AdministrationController : NewContr
    {
        public ActionResult openAdministrationMain()
        {
            return CheckRole(View("~/Views/Administration/Main.cshtml"), "admin");
        }
    }
}