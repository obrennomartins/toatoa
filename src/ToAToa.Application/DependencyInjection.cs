using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToAToa.Application.Decorators;
using ToAToa.DataAccess.Repositories;
using ToAToa.Domain.Interfaces;

namespace ToAToa.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection service, IConfiguration configuration)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        service.AddMediatR(mediatRConfiguration => mediatRConfiguration.RegisterServicesFromAssembly(assembly));
        service.AddAutoMapper(mapperConfiguration =>
        {
            var licenseKey = configuration["AutoMapper:LicenseKey"];

            if (!string.IsNullOrWhiteSpace(licenseKey))
            {
                mapperConfiguration.LicenseKey = licenseKey;
            }
        }, assembly);
        service.AddMemoryCache();

        service.AddScoped<IAtividadeRepository>(provider =>
        {
            var originalRepository = provider.GetRequiredService<AtividadeRepository>();
            var cache = provider.GetRequiredService<ICacheService>();
            return new CachedAtividadeRepository(originalRepository, cache);
        });
    }
}
