using Livraria.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Livraria.Infra.Database.Mapping;

public class AvaliacaoMapping : IEntityTypeConfiguration<Avaliacao>
{
    public void Configure(EntityTypeBuilder<Avaliacao> builder)
    {
        builder.HasKey(e => e.Id).HasName("avaliacao_pkey");

        builder.ToTable("avaliacao");

        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.Descricao).HasColumnName("avaliacao");
        builder.Property(e => e.LivroId).HasColumnName("livro_id");
        builder.Property(e => e.Nome)
            .HasMaxLength(50)
            .HasColumnName("nome");
        builder.Property(e => e.Nota).HasColumnName("nota");

        builder.HasOne(d => d.Livro).WithMany(p => p.Avaliacao)
            .HasForeignKey(d => d.LivroId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_avaliacao_livros");
    }
}
