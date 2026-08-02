using ToAToa.DataAccess;
using ToAToa.Presentation.Configurations;
using ToAToa.Presentation.Endpoints;
using ToAToa.Presentation.Middlewares;

namespace ToAToa.Presentation;

public static class DependencyInjection
{
    public static void AddPresentation(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddForwardedHeadersConfiguration(configuration);

        service.AddCors(options =>
        {
            options.AddPolicy("AllowAnyOrigin",
                builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
        });

        service.AddEndpointsApiExplorer();

        service.AddHealthChecks()
            .AddDbContextCheck<ToAToaDbContext>();

        service.AddSwaggerConfiguration();
    }

    public static void UsePresentation(this WebApplication app)
    {
        app.UseForwardedHeaders();
        app.UseMiddleware<RemoveNulosVaziosMiddleware>();
        app.UseCors("AllowAnyOrigin");
        app.MapHealthChecks("/health");
        app.RegisterAtividadeEndpoints();
        app.UseSwaggerConfiguration();
    }

    public static void AddPresentationLogging(this ILoggingBuilder logging) { }
}
