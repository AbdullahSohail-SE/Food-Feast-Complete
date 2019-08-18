using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Data.SqlClient;
using Food_Feast.Models;
namespace Food_Feast
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            var db=new DB();
            var deltedItems = db.ExeccuteCommandAffectedWithoutParameter("delete_all_from_cart");
        }

        protected void Session_Start(Object sender, EventArgs e)
        {
            HttpContext.Current.Session["loggedIn"] = false;
            HttpContext.Current.Session["loginId"] = 0;
            HttpContext.Current.Session["loginName"] = "";
            HttpContext.Current.Session["itemsInCart"] = 0;
        }
    }
}
