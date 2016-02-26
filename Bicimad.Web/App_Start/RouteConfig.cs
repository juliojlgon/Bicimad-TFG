using System.Web.Mvc;
using System.Web.Routing;

namespace Bicimad.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //ACOUNT
            routes.MapRoute("Account", "cuenta/{action}/{id}",
                new {controller = "Account", action = "Index", id = UrlParameter.Optional});

            routes.MapRoute("Default", "{controller}/{action}/{id}",
                new {controller = "Home", action = "Index", id = UrlParameter.Optional}
                );
        }
    }
}