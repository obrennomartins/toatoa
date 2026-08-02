using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ToAToa.Presentation.Configurations;

namespace ToAToa.Tests;

public class ForwardedHeadersConfigurationsTests
{
    [Fact]
    public void DeveConfigurarOsProxiesConhecidos()
    {
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["ReverseProxy:KnownProxies:0"] = "192.0.2.10",
                ["ReverseProxy:KnownProxies:1"] = "2001:db8::10"
            })
            .Build();
        var services = new ServiceCollection();

        services.AddForwardedHeadersConfiguration(configuration);

        using var serviceProvider = services.BuildServiceProvider();
        var options = serviceProvider
            .GetRequiredService<IOptions<ForwardedHeadersOptions>>()
            .Value;

        Assert.Equal(ForwardedHeaders.All, options.ForwardedHeaders);
        Assert.Empty(options.KnownNetworks);
        Assert.Collection(
            options.KnownProxies,
            proxy => Assert.Equal(IPAddress.Parse("192.0.2.10"), proxy),
            proxy => Assert.Equal(IPAddress.Parse("2001:db8::10"), proxy));
    }

    [Fact]
    public void DeveRejeitarEnderecoDeProxyInvalido()
    {
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["ReverseProxy:KnownProxies:0"] = "endereco-invalido"
            })
            .Build();
        var services = new ServiceCollection();

        var exception = Assert.Throws<InvalidOperationException>(() =>
            services.AddForwardedHeadersConfiguration(configuration));

        Assert.Contains("ReverseProxy:KnownProxies", exception.Message);
    }

    [Theory]
    [InlineData("192.0.2.10", "/toatoa")]
    [InlineData("198.51.100.10", "")]
    public async Task DeveAceitarPrefixoSomenteDoProxyConhecido(
        string remoteIpAddress,
        string expectedPathBase)
    {
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["ReverseProxy:KnownProxies:0"] = "192.0.2.10"
            })
            .Build();
        var services = new ServiceCollection();
        services.AddLogging();
        services.AddForwardedHeadersConfiguration(configuration);

        using var serviceProvider = services.BuildServiceProvider();
        PathString observedPathBase = default;
        var applicationBuilder = new ApplicationBuilder(serviceProvider);
        applicationBuilder.UseForwardedHeaders();
        applicationBuilder.Run(context =>
        {
            observedPathBase = context.Request.PathBase;
            return Task.CompletedTask;
        });
        var application = applicationBuilder.Build();
        var context = new DefaultHttpContext
        {
            RequestServices = serviceProvider
        };
        context.Connection.RemoteIpAddress = IPAddress.Parse(remoteIpAddress);
        context.Request.Headers["X-Forwarded-Prefix"] = "/toatoa";

        await application(context);

        Assert.Equal(expectedPathBase, observedPathBase.Value ?? string.Empty);
    }
}
