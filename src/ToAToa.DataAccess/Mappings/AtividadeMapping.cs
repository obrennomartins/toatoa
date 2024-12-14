using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToAToa.Domain.Entities;

namespace ToAToa.DataAccess.Mappings;

public class AtividadeMapping : IEntityTypeConfiguration<Atividade>
{
    public void Configure(EntityTypeBuilder<Atividade> builder)
    {
        builder.ToTable("Atividade");

        builder.HasKey(atividade => atividade.Id);

        builder.Property(atividade => atividade.Descricao)
            .IsRequired()
            .HasColumnType("text")
            .HasMaxLength(50);
    }
}
