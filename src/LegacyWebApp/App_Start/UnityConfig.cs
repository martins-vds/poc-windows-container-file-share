using ConfigurationManagerProvider;
using Microsoft.Extensions.Configuration;
using System.IO.Abstractions;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace LegacyWebApp
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<IFileSystem, FileSystem>();

            container.RegisterFactory<IConfiguration>(c =>
            {
                return new ConfigurationBuilder()
                            .AddConfigurationManager()
                            .AddJsonFile("appsettings.json")
                            .Build();
            }, FactoryLifetime.Singleton);

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}