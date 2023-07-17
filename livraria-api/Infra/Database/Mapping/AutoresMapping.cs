using Livraria.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Livraria.Infra.Database.Mapping;

public class AutoresMapping : IEntityTypeConfiguration<Autores>
{
    public void Configure(EntityTypeBuilder<Autores> builder)
    {
        builder.HasKey(e => e.Id).HasName("autores_pkey");

        builder.ToTable("autores");

        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.Email)
            .HasMaxLength(50)
            .HasColumnName("email");
        builder.Property(e => e.Nome)
            .HasMaxLength(50)
            .HasColumnName("nome");
        builder.Property(e => e.Telefone)
            .HasMaxLength(50)
            .HasColumnName("telefone");
    }
}
