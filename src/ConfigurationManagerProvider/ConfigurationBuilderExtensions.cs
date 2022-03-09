using Microsoft.Extensions.Configuration;

namespace ConfigurationManagerProvider
{
    public static class ConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddConfigurationManager(
            this IConfigurationBuilder builder)
        {
            return builder.Add(new ConfigurationManagerSource());
        }
    }
}