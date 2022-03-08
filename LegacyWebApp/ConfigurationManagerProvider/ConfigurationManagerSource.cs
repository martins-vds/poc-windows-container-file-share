using Microsoft.Extensions.Configuration;

namespace ConfigurationManagerProvider
{
    public class ConfigurationManagerSource : IConfigurationSource
    {
        public IConfigurationProvider Build(IConfigurationBuilder builder) =>
            new ConfigurationManagerConfigurationProvider();
    }
}