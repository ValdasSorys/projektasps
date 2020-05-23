using FluentScheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebApplication6.Controllers;

namespace WebApplication6
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            var registry = new Registry();
            registry.Schedule(() =>
                performSearch()
                ).ToRunNow().AndEvery(5).Seconds();
            JobManager.Initialize(registry);
        }

        public void performSearch()
        {
            var controller = new MatchCreationController();
            controller.initiateSearch();
            controller = null;
        }

    }
}
