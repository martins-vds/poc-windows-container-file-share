using Swashbuckle.Application;
using System;
using System.Linq;
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
                handler: new RedirectHandler((message =>
                {
                    if (message.Headers.TryGetValues("X-Forwarded-Proto", out var list))
                    {
                        if (list != null && list.Any(l => l.Equals("https", StringComparison.InvariantCultureIgnoreCase)))
                        {
                            return message.RequestUri.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port, UriFormat.UriEscaped).ToString();
                        }
                    }
                    
                    return message.RequestUri.ToString();
                }), "swagger")
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
