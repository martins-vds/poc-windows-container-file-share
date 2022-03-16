using System.Web.Http;
using Unity.Mvc5;

namespace LegacyWebApp
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            config.MapHttpAttributeRoutes();

            //ASP.NET Web API Route Config
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { 
                    id = RouteParameter.Optional 
                }
            );
        }
    }
}
