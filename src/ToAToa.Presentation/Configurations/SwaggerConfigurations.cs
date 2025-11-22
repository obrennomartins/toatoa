using Microsoft.OpenApi.Models;

namespace ToAToa.Presentation.Configurations;

public static class SwaggerConfigurations
{
    public static void AddSwaggerConfiguration(this IServiceCollection service)
    {
        service.AddSwaggerGen(options =>
        {
            var configuration = service.BuildServiceProvider().GetRequiredService<IConfiguration>();
            var absoluteServerPath = configuration["SERVER:ABSOLUTE_PATH"]?.TrimEnd('/') ?? "";
            var xmlFileName = $"{typeof(DependencyInjection).Assembly.GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));

            if (!string.IsNullOrEmpty(absoluteServerPath))
            {
                options.AddServer(new OpenApiServer
                {
                    Url = absoluteServerPath,
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

    public static void UseSwaggerConfiguration(this WebApplication app)
    {
        var relativeServerPath = app.Configuration["SERVER:RELATIVE_PATH"]?.TrimEnd('/') ?? string.Empty;

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint($"{relativeServerPath}/swagger/v1/swagger.json", "Tô à toa API v1");
        });
    }
}
