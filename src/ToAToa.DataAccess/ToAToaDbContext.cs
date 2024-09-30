using Microsoft.EntityFrameworkCore;
using ToAToa.Domain.Entities;

namespace ToAToa.DataAccess;

public class ToAToaDbContext(DbContextOptions<ToAToaDbContext> options) : DbContext(options)
{
    static ToAToaDbContext()
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
    
    public DbSet<Atividade> Atividades { get; init; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ToAToaDbContext).Assembly);

        ToAToaDbSeeder.Seeder(modelBuilder);
        
        base.OnModelCreating(modelBuilder);
    }
}
