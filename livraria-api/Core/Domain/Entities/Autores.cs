using Livraria.Core.Domain.Models;

namespace Livraria.Core.Domain.Entities;

public class Autores : EntidadeBase<Autores>
{
    public int Id { get; private set; }

    public string Nome { get; private set; } = null!;

    public string Email { get; private set; } = null!;

    public string Telefone { get; private set; } = null!;

    public virtual ICollection<Livros> Livros { get; private set; } = new List<Livros>();

    protected Autores() { }

    public Autores(CadastroAutorModel modelo)
    {
        this.Nome = modelo.Nome;
        this.Email = modelo.Email;
        this.Telefone = modelo.Telefone;
    }

    public void Atualizar(AtualizaAutorModel modelo)
    {
        this.Nome = modelo.Nome;
        this.Email = modelo.Email;
        this.Telefone = modelo.Telefone;
    }

    protected override void ValidateState() { }

}
