using Livraria.Core.Domain.Models;

namespace Livraria.Core.Domain.Entities;

public class Clientes : EntidadeBase<Clientes>
{
    public int Id { get; private set; }

    public string Nome { get; private set; } = null!;

    public string Email { get; private set; } = null!;

    public string Senha { get; private set; } = null!;

    public string Telefone { get; private set; } = null!;

    public string Endereco { get; private set; } = null!;

    public virtual ICollection<Vendas> Vendas { get; private set; } = new List<Vendas>();

    protected Clientes() { }

    public Clientes(CadastroClienteModel modelo)
    {
        this.Nome = modelo.Nome;
        this.Email = modelo.Email;
        this.Senha = modelo.Senha;
        this.Telefone = modelo.Telefone;
        this.Endereco = modelo.Endereco;
    }

    public void Atualizar(AtualizaClienteModel modelo)
    {
        this.Nome = modelo.Nome;
        this.Email = modelo.Email;
        this.Senha = modelo.Senha;
        this.Telefone = modelo.Telefone;
        this.Endereco = modelo.Endereco;
    }

    protected override void ValidateState() { }
}
