namespace Eventer.Web
{
    using System.Web.Mvc;
    using System.Web.Routing;

    using Eventer.Web.Infrastructure.Constraints;

    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
                name: "JoinEvent",
                url: "Join/{id}",
                defaults: new { controller = "Events", action = "Join" }
            );

            routes.MapRoute(
                name: "ByDate",
                url: "Events/Show/{date}/{slug}",
                defaults: new { controller = "Events", action = "Show"}
            );

            routes.MapRoute(
                name: "Events",
                url: "Events/{action}/{slug}",
                defaults: new { controller = "Events", action = "Index", slug = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Manage",
                url: "Manage/{action}",
                defaults: new { controller = "Manage", action = "Index" }
            );

            routes.MapRoute(
                name: "Home",
                url: "{action}",
                defaults: new { controller = "Home" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}