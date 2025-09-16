using Microsoft.Extensions.Configuration;

namespace EatWise.Common.Infrastructure.Configuration;

public static class ConfigurationExtensions
{
    public static string GetConnectionStringOrThrow(this IConfiguration configuration, string name)
    {
        return configuration.GetConnectionString(name) ?? throw new InvalidOperationException($"The connection string '{name}' does not exist.");
    }

    public static T GetValueOrThrow<T>(this IConfiguration configuration, string name)
    {
        return configuration.GetValue<T>(name) ?? throw new InvalidOperationException($"The value '{name}' does not exist.");
    }
}
