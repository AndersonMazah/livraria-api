using Livraria.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Livraria.Infra.Database.Mapping;

public class VendasMapping : IEntityTypeConfiguration<Vendas>
{
    public void Configure(EntityTypeBuilder<Vendas> builder)
    {
        builder.HasKey(e => e.Id).HasName("vendas_pkey");

        builder.ToTable("vendas");

        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.ClienteId).HasColumnName("cliente_id");
        builder.Property(e => e.Data)
            .HasColumnType("timestamp without time zone")
            .HasColumnName("data");
        builder.Property(e => e.LivroId).HasColumnName("livro_id");
        builder.Property(e => e.Valor)
            .HasPrecision(12, 2)
            .HasColumnName("valor");

        builder.HasOne(d => d.Cliente).WithMany(p => p.Vendas)
            .HasForeignKey(d => d.ClienteId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_vendas_clientes");

        builder.HasOne(d => d.Livro).WithMany(p => p.Vendas)
            .HasForeignKey(d => d.LivroId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_vendas_livros");
    }
}
