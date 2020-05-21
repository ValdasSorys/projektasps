using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApplication6
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "AdministrationNewsCreate",
                url: "admin/news/create",
                defaults: new { controller = "AdministrationNews", action = "openAdministrationNewsCreate" }
            );

            routes.MapRoute(
                name: "AdministrationNewsEdit",
                url: "admin/news/edit/{id}",
                defaults: new { controller = "AdministrationNews", action = "openAdministrationNewsEdit" }
            );

            routes.MapRoute(
                name: "AdministrationNewsDelete",
                url: "admin/news/delete/{id}",
                defaults: new { controller = "AdministrationNews", action = "deleteNewsArticle" }
            );

            routes.MapRoute(
                name: "AdministrationNewsList",
                url: "admin/news",
                defaults: new { controller = "AdministrationNews", action = "openAdministrationNewsList" }
            );

            routes.MapRoute(
                name: "AdministrationMain",
                url: "admin",
                defaults: new { controller = "Administration", action = "openAdministrationMain" }
            );

            routes.MapRoute(
                name: "Login",
                url: "login",
                defaults: new { controller = "TestLogin", action = "Index" }
            );

            routes.MapRoute(
                name: "DefaultGamer",
                url: "IndexGamer",
                defaults: new { controller = "Home", action = "IndexGamer" }
                );

            routes.MapRoute(
               name: "DefaultAdmin",
               url: "Index",
               defaults: new { controller = "Home", action = "Index" }
               );

            routes.MapRoute(
                name: "LoginUser",
                url: "loginUser",
                defaults: new { controller = "TestLogin", action = "LoginUser" }
                );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
