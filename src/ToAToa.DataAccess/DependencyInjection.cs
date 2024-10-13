using Microsoft.Extensions.DependencyInjection;
using ToAToa.DataAccess.Repositories;
using ToAToa.Domain.Interfaces;

namespace ToAToa.DataAccess;

public static class DependencyInjection
{
    public static void AddDataAccess(this IServiceCollection service)
    {
        service.AddDbContext<ToAToaDbContext>();

        // Repositórios
        service.AddScoped<IAtividadeRepository, AtividadeRepository>();
    }
}
