using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Configuration;

namespace ConfigurationManagerProvider
{
    public class ConfigurationManagerConfigurationProvider : ConfigurationProvider
    {
        public override void Load()
        {
            var configuration = new Dictionary<string, string>();

            foreach (var key in System.Configuration.ConfigurationManager.AppSettings.AllKeys)
            {
                configuration.Add(key, System.Configuration.ConfigurationManager.AppSettings[key]);
            }

            foreach (ConnectionStringSettings connectionString in System.Configuration.ConfigurationManager.ConnectionStrings)
            {
                configuration.Add($"ConnectionStrings:{connectionString.Name}", connectionString.ConnectionString);
            }

            Data = configuration;
        }
    }
}