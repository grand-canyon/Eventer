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
                name: "Event",
                url: "Events/{action}/{date}/{slug}",
                defaults: new { controller = "Events", action = "Show", slug = UrlParameter.Optional },
                constraints: new { date = new DateConstraint() }
            );

            routes.MapRoute(
                name: "Events",
                url: "Events/{action}",
                defaults: new { controller = "Events", action = "Index" }
            );

            routes.MapRoute(
                name: "StaticPages",
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