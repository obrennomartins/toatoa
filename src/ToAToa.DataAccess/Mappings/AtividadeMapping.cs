using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToAToa.Domain.Entities;

namespace ToAToa.DataAccess.Mappings;

public class AtividadeMapping : IEntityTypeConfiguration<Atividade>
{
    public void Configure(EntityTypeBuilder<Atividade> builder)
    {
        builder.ToTable("ATIVIDADE");

        builder.HasKey(atividade => atividade.Id);

        builder.Property(atividade => atividade.Id)
            .HasColumnName("ID");

        builder.Property(atividade => atividade.Descricao)
            .HasColumnName("DESCRICAO")
            .IsRequired()
            .HasColumnType("NVARCHAR2(50)")
            .HasMaxLength(50);

        builder.Property(atividade => atividade.Ativo)
            .HasColumnName("ATIVO");
    }
}
