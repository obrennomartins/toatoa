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
        var connectionString = configuration.GetConnectionString("ORACLECONNSTR_ToAToaDb");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                "Connection string 'ConnectionStrings:ORACLECONNSTR_ToAToaDb' is not configured. " +
                "Set it with user-secrets or the 'ConnectionStrings__ORACLECONNSTR_ToAToaDb' environment variable.");
        }

        service.AddDbContext<ToAToaDbContext>(options =>
            options.UseOracle(
                connectionString,
                oracleOptions => oracleOptions.UseOracleSQLCompatibility(OracleSQLCompatibility.DatabaseVersion23)));

        service.AddScoped<ICacheService, CacheService>();

        // Repositórios
        service.AddScoped<AtividadeRepository>();
    }
}
