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
        
        modelBuilder.Entity<Atividade>().HasData(
            new Atividade(1, "Enrolar brigadeiro"),
            new Atividade(2, "Fazer uma roda de samba"),
            new Atividade(3, "Jogar capoeira"),
            new Atividade(4, "Preparar uma feijoada"),
            new Atividade(5, "Dançar forró"),
            new Atividade(6, "Assistir à novela das 9"),
            new Atividade(7, "Fazer compras na feira livre"),
            new Atividade(8, "Participar de uma festa junina"),
            new Atividade(9, "Aprender a tocar cavaquinho"),
            new Atividade(10, "Fazer um churrasco")
        );
        
        base.OnModelCreating(modelBuilder);
    }
}
