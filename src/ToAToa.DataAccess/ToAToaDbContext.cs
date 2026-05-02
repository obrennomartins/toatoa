using Microsoft.EntityFrameworkCore;
using ToAToa.Domain.Entities;

namespace ToAToa.DataAccess;

public class ToAToaDbContext(DbContextOptions<ToAToaDbContext> options) : DbContext(options)
{
    public DbSet<Atividade> Atividades { get; init; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("TOATOA");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ToAToaDbContext).Assembly);
        ToAToaDbSeeder.Seeder(modelBuilder);
        
        base.OnModelCreating(modelBuilder);
    }
}
