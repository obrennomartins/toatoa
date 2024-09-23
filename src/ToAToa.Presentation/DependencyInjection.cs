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
                Contact = new OpenApiContact
                {
                    Name = "Brenno Martins",
                    Url = new Uri("https://tal.etc.br/eu")
                }
            });
        });
    }

    public static void UsePresentation(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Tô à toa API v1");
            options.RoutePrefix = "";
        });
    }
}
