using Livraria.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Livraria.Infra.Database.Mapping;

public class LivrosMapping : IEntityTypeConfiguration<Livros>
{
    public void Configure(EntityTypeBuilder<Livros> builder)
    {
        builder.HasKey(e => e.Id).HasName("livros_pkey");

        builder.ToTable("livros");

        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.AutorId).HasColumnName("autor_id");
        builder.Property(e => e.Descricao).HasColumnName("descricao");
        builder.Property(e => e.Editora)
            .HasMaxLength(50)
            .HasColumnName("editora");
        builder.Property(e => e.Estoque).HasColumnName("estoque");
        builder.Property(e => e.Nome)
            .HasMaxLength(50)
            .HasColumnName("nome");
        builder.Property(e => e.Paginas).HasColumnName("paginas");
        builder.Property(e => e.Valor)
            .HasPrecision(12, 2)
            .HasColumnName("valor");

        builder.HasOne(d => d.Autor).WithMany(p => p.Livros)
            .HasForeignKey(d => d.AutorId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_livros_autores");
    }
}
