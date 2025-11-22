using Microsoft.Extensions.DependencyInjection;
using ToAToa.Application.Decorators;
using ToAToa.DataAccess.Repositories;
using ToAToa.Domain.Interfaces;

namespace ToAToa.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection service)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        service.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(assembly));
        service.AddAutoMapper(assembly);
        service.AddMemoryCache();

        service.AddScoped<IAtividadeRepository>(provider =>
        {
            var originalRepository = provider.GetRequiredService<AtividadeRepository>();
            var cache = provider.GetRequiredService<ICacheService>();
            return new CachedAtividadeRepository(originalRepository, cache);
        });
    }
}
