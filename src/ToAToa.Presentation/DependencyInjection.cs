using Microsoft.OpenApi.Models;

namespace ToAToa.Presentation;

public static class DependencyInjection
{
    public static void AddPresentation(this IServiceCollection service)
    {
        service.AddEndpointsApiExplorer();
        
        service.AddSwaggerGen(options =>
        {
            var presentationAssembly = typeof(DependencyInjection).Assembly;
            var xmlFileName = $"{presentationAssembly.GetName().Name}.xml";
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

    public static void UsePresentation(this IApplicationBuilder app)
    {
        var apiBasePath = Environment.GetEnvironmentVariable("API_BASE_PATH") ?? "";

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint($"{apiBasePath}/swagger/v1/swagger.json", "Tô à toa API v1");
            options.RoutePrefix = string.IsNullOrEmpty(apiBasePath) ? "swagger" : $"{apiBasePath}/swagger";
        });
    }
}
