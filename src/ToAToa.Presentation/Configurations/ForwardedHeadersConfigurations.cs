using System.Net;
using Microsoft.AspNetCore.HttpOverrides;

namespace ToAToa.Presentation.Configurations;

public static class ForwardedHeadersConfigurations
{
    public static void AddForwardedHeadersConfiguration(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var knownProxies = configuration
            .GetSection("ReverseProxy:KnownProxies")
            .GetChildren()
            .Select(section => section.Value)
            .Where(value => !string.IsNullOrWhiteSpace(value))
            .Select(value => IPAddress.TryParse(value, out var address)
                ? address
                : throw new InvalidOperationException(
                    $"O endereço '{value}' configurado em ReverseProxy:KnownProxies não é um IP válido."))
            .ToArray();

        services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders = ForwardedHeaders.All;

            if (knownProxies.Length == 0)
            {
                return;
            }

            options.KnownNetworks.Clear();
            options.KnownProxies.Clear();

            foreach (var knownProxy in knownProxies)
            {
                options.KnownProxies.Add(knownProxy);
            }
        });
    }
}
