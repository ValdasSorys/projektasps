using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication6.Controllers
{
    public abstract class NewContr : Controller
    {
        public ActionResult CheckRole(ActionResult result, string role)
        {
            if (role == "admin" || role == "user")
            {
                if (System.Web.HttpContext.Current.Session["role"] == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                if (System.Web.HttpContext.Current.Session["role"].ToString() != role)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                if (System.Web.HttpContext.Current.Session["role"] == null)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return result;
        }
    }
}