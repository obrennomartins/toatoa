using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ToAToa.Application;
using ToAToa.DataAccess;
using ToAToa.Domain;
using ToAToa.Domain.Entities;
using ToAToa.Domain.Interfaces;
using ToAToa.Presentation;

namespace ToAToa.Tests;

public class PresentationEndpointsTests
{
    [Fact]
    public async Task HealthCheck_DeveContinuarFuncionando()
    {
        var builder = CreateBuilder();
        builder.Services.AddDbContext<ToAToaDbContext>(options =>
            options.UseInMemoryDatabase($"toatoa-health-{Guid.NewGuid()}"));
        builder.Services.AddDomain();
        builder.Services.AddApplication(builder.Configuration);
        builder.Services.AddPresentation(builder.Configuration);

        await using var app = await StartApplicationAsync(builder);
        using var client = app.GetTestClient();

        var response = await client.GetAsync("/health");
        var body = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("Healthy", body);
    }

    [Fact]
    public async Task AtividadeAleatoria_DeveContinuarFuncionandoComMediatREAutoMapper()
    {
        var builder = CreateBuilder();
        builder.Services.AddDomain();
        builder.Services.AddApplication(builder.Configuration);
        builder.Services.RemoveAll<IAtividadeRepository>();
        builder.Services.AddSingleton<IAtividadeRepository>(
            new FakeAtividadeRepository(new Atividade(1, "Atividade do teste")));
        builder.Services.AddPresentation(builder.Configuration);

        await using var app = await StartApplicationAsync(builder);
        using var client = app.GetTestClient();

        var response = await client.GetAsync("/api/v1/atividades/aleatoria");
        var body = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        using var document = JsonDocument.Parse(body);
        var root = document.RootElement;

        Assert.True(root.GetProperty("isSuccess").GetBoolean());
        Assert.Equal("Atividade do teste", root.GetProperty("data").GetProperty("descricao").GetString());
    }

    private static WebApplicationBuilder CreateBuilder()
    {
        var builder = WebApplication.CreateBuilder();
        builder.WebHost.UseTestServer();
        builder.Configuration.AddInMemoryCollection(new Dictionary<string, string?>
        {
            ["ReverseProxy:KnownProxies:0"] = "127.0.0.1"
        });

        return builder;
    }

    private static async Task<WebApplication> StartApplicationAsync(WebApplicationBuilder builder)
    {
        var app = builder.Build();
        app.UsePresentation();
        await app.StartAsync();

        return app;
    }

    private sealed class FakeAtividadeRepository(Atividade atividade) : IAtividadeRepository
    {
        public Task<Atividade?> ObterAtividadeAleatoriaAsync() =>
            Task.FromResult<Atividade?>(atividade);

        public Task<int> ObterTotalAtividadesAsync() =>
            Task.FromResult(1);

        public Task<Atividade?> ObterAtividadePorSkipAsync(int skip) =>
            Task.FromResult<Atividade?>(atividade);
    }
}
