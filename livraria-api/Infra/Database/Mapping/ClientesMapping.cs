using Livraria.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Livraria.Infra.Database.Mapping;

public class ClientesMapping : IEntityTypeConfiguration<Clientes>
{
    public void Configure(EntityTypeBuilder<Clientes> builder)
    {
        builder.HasKey(e => e.Id).HasName("clientes_pkey");

        builder.ToTable("clientes");

        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.Email)
            .HasMaxLength(50)
            .HasColumnName("email");
        builder.Property(e => e.Endereco)
            .HasMaxLength(100)
            .HasColumnName("endereco");
        builder.Property(e => e.Nome)
            .HasMaxLength(50)
            .HasColumnName("nome");
        builder.Property(e => e.Senha)
            .HasMaxLength(30)
            .HasColumnName("senha");
        builder.Property(e => e.Telefone)
            .HasMaxLength(50)
            .HasColumnName("telefone");
    }
}
