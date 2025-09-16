using EatWise.Common.Infrastructure.Configuration;

namespace EatWise.API.Extensions;

internal static class KeyCloakHealthChecksBuilderExtensions
{
    private const string KeyCloakHealthCheck = "KeyCloak";
    private const string KeyCloakHealthUrl = "KeyCloak:HealthUrl";

    internal static IHealthChecksBuilder AddKeyCloak(this IHealthChecksBuilder builder, Uri healthUri)
    {
        builder.AddUrlGroup(healthUri, HttpMethod.Get, KeyCloakHealthCheck);
        
        return builder;
    }

    internal static Uri GetKeyCloakHealthUrl(this IConfiguration configutarion)
    {
        return new Uri(configutarion.GetValueOrThrow<string>(KeyCloakHealthUrl)!);
    }
}
