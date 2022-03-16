using Swashbuckle.Application;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace LegacyWebApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Set Swagger as default start page
            routes.MapHttpRoute(
                name: "swagger_root",
                routeTemplate: "",
                defaults: null,
                constraints: null,
                handler: new RedirectHandler((message => message.RequestUri.ToString()), "swagger")
            );

            //ASP.NET MVC Route Config
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "swagger", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
