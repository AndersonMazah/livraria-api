using Livraria.Core.Domain.Models;

namespace Livraria.Core.Domain.Entities;

public class Livros : EntidadeBase<Livros>
{
    public int Id { get; private set; }

    public string Nome { get; private set; } = null!;

    public decimal Valor { get; private set; }

    public string? Descricao { get; private set; }

    public int? Paginas { get; private set; }

    public string? Editora { get; private set; }

    public int Estoque { get; private set; }

    public int AutorId { get; private set; }

    public virtual Autores Autor { get; private set; } = null!;

    public virtual ICollection<Avaliacao> Avaliacao { get; private set; } = new List<Avaliacao>();

    public virtual ICollection<Vendas> Vendas { get; private set; } = new List<Vendas>();

    protected Livros() { }

    public Livros(CadastroLivroModel modelo)
    {
        this.Nome = modelo.Nome;
        this.Valor = modelo.Valor;
        this.AutorId = modelo.AutorId;
    }

    public void Atualizar(AtualizaLivroModel modelo)
    {
        this.Valor = modelo.Valor;
    }

    public void AtualizarInfo(InfoLivroModel modelo)
    {
        this.Descricao = modelo.Descricao;
        this.Paginas = modelo.Paginas;
        this.Editora = modelo.Editora;
    }

    protected override void ValidateState() { }

    public void DiminuirEstoque() => this.Estoque--;
}
