using System.Web.Mvc;
using System.Web.Routing;

namespace Reference.Web
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Error - 404",
                "NotFound",
                new { controller = "Error", action = "NotFound" });

            routes.MapRoute(
                "Error - 500",
                "ServerError",
                new { controller = "Error", action = "ServerError" });

            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}