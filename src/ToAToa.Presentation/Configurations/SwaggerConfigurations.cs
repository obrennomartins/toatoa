using Microsoft.OpenApi.Models;

namespace ToAToa.Presentation.Configurations;

public static class SwaggerConfigurations
{
    public static void AddSwaggerConfiguration(this IServiceCollection service)
    {
        service.AddSwaggerGen(options =>
        {
            var xmlFileName = $"{typeof(DependencyInjection).Assembly.GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));

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

    public static void UseSwaggerConfiguration(this WebApplication app)
    {
        app.UseSwagger(options =>
        {
            options.PreSerializeFilters.Add((document, request) =>
            {
                var pathBase = request.PathBase.Value?.TrimEnd('/');
                document.Servers =
                [
                    new OpenApiServer
                    {
                        Url = string.IsNullOrEmpty(pathBase) ? "/" : pathBase
                    }
                ];
            });
        });

        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("v1/swagger.json", "Tô à toa API v1");
        });
    }
}
