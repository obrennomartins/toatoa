using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ToAToa.DataAccess.Repositories;
using ToAToa.Domain.Interfaces;

namespace ToAToa.DataAccess;

public static class DependencyInjection
{
    public static void AddDataAccess(this IServiceCollection service)
    {
        service.AddDbContext<ToAToaDbContext>(options =>
        {
            options.UseInMemoryDatabase("ToAToa");
        });
        
        var options = new DbContextOptionsBuilder<ToAToaDbContext>()
            .UseInMemoryDatabase("ToAToa")
            .Options;
        
        using var context = new ToAToaDbContext(options);
        context.Database.EnsureCreated();

        // Repositórios
        service.AddScoped<IAtividadeRepository, AtividadeRepository>();
    }
}
