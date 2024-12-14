using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ToAToa.Domain.Entities;

namespace ToAToa.DataAccess;

public class ToAToaDbContext(DbContextOptions<ToAToaDbContext> options, IConfiguration configuration) : DbContext(options)
{
    static ToAToaDbContext()
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public DbSet<Atividade> Atividades { get; init; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(configuration["POSTGRESQLCONNSTR_ToAToaDb"]);

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ToAToaDbContext).Assembly);

        ToAToaDbSeeder.Seeder(modelBuilder);
        
        base.OnModelCreating(modelBuilder);
    }
}
