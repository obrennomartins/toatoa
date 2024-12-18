using Microsoft.OpenApi.Models;
using ToAToa.Presentation.Endpoints;
using ToAToa.Presentation.Middlewares;

namespace ToAToa.Presentation;

public static class DependencyInjection
{
    public static void AddPresentation(this IServiceCollection service)
    {
        service.AddEndpointsApiExplorer();

        service.AddHealthChecks();

        service.AddSwaggerGen(options =>
        {
            var basePath = Environment.GetEnvironmentVariable("API_BASE_PATH") ?? "";   
            
            var presentationAssembly = typeof(DependencyInjection).Assembly;
            var xmlFileName = $"{presentationAssembly.GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));

            if (!string.IsNullOrEmpty(basePath))
            {
                options.AddServer(new OpenApiServer
                {
                    Url = basePath
                });
            }
            
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Tô à toa",
                Description = "Uma API simples para ajudar você a encontrar atividades para fazer.",
                License = new OpenApiLicense
                {
                    Name = "MIT"
                },
                Contact = new OpenApiContact
                {
                    Name = "Brenno Martins"
                }
            });
        });
    }

    public static void UsePresentation(this WebApplication app)
    {
        var basePath = Environment.GetEnvironmentVariable("API_BASE_PATH") ?? string.Empty;
        
        app.UseMiddleware<RemoveNulosVaziosMiddleware>();

        app.MapHealthChecks("/health");

        app.RegisterAtividadeEndpoints();
        
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint($"{basePath}/swagger/v1/swagger.json", "Tô à toa API v1");
        });
    }
}
