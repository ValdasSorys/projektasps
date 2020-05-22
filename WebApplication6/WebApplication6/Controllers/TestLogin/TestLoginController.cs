using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication6.Controllers.Test
{
    public class TestLoginController : Controller
    {
        private static string ConnectionString = ConfigurationManager.ConnectionStrings["mySQLConnection"].ConnectionString;
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
            string userRole = UserRole(x);
            ViewBag.Message = userRole;
            if (userRole == "")
            {
                return View("~/Views/Test/View.cshtml");
            }
            Session["id"] = x;
            Session["role"] = userRole;
            return RedirectToAction("Index", "Home");   
        }

        public ActionResult LogoutUser()
        {
            Session["id"] = null;
            Session["role"] = null;
            return View("~/Views/Test/View.cshtml");
        }

        public string UserRole(int id)
        {
            string role = "";
            var Connection = new MySqlConnection(ConnectionString);
            Connection.Open();
            string SQLStatement = "SELECT * FROM ISuser Where id = " + id;
            var Command = new MySqlCommand(SQLStatement, Connection);
            MySqlDataReader Reader = Command.ExecuteReader();

            if (!Reader.HasRows)
            {
                return role;
            }
            Reader.Close();
            SQLStatement = "SELECT * FROM administrators Where id = " + id;
            Command = new MySqlCommand(SQLStatement, Connection);
            Reader = Command.ExecuteReader();
            if (Reader.HasRows)
            {
                role = "admin";
            }
            else
            {
                role = "user";
            }
            Reader.Close();
            Connection.Close();
            return role;
        }
    }
}