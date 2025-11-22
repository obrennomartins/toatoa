using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToAToa.DataAccess.Caching;
using ToAToa.DataAccess.Repositories;
using ToAToa.Domain.Interfaces;

namespace ToAToa.DataAccess;

public static class DependencyInjection
{
    public static void AddDataAccess(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddDbContext<ToAToaDbContext>(options =>
            options.UseNpgsql(configuration["ConnectionStrings:POSTGRESQLCONNSTR_ToAToaDb"]));

        service.AddScoped<ICacheService, CacheService>();

        // Repositórios
        service.AddScoped<AtividadeRepository>();
    }
}
