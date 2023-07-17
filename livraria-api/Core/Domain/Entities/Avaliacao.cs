using Livraria.Core.Domain.Models;

namespace Livraria.Core.Domain.Entities;

public class Avaliacao : EntidadeBase<Avaliacao>
{
    public int Id { get; private set; }

    public string Nome { get; private set; } = null!;

    public int Nota { get; private set; }

    public string Descricao { get; private set; } = null!;

    public int LivroId { get; private set; }

    public virtual Livros Livro { get; private set; } = null!;

    protected Avaliacao() { }

    public Avaliacao(CadastroAvaliacaoModel modelo)
    {
        this.Nome = modelo.Nome;
        this.Nota = modelo.Nota;
        this.Descricao = modelo.Descricao;
        this.LivroId = modelo.LivroId;
    }

    public void Atualizar(AtualizaAvaliacaoModel modelo)
    {
        this.Nome = modelo.Nome;
        this.Nota = modelo.Nota;
        this.Descricao = modelo.Descricao;
        this.LivroId = modelo.LivroId;
    }

    protected override void ValidateState() { }
}
