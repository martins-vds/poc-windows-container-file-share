using ConfigurationManagerProvider;
using Microsoft.Extensions.Configuration;

using System.IO.Abstractions;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

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

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}