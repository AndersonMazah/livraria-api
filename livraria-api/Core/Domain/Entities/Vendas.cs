using Livraria.Core.Domain.Models;

namespace Livraria.Core.Domain.Entities;

public class Vendas : EntidadeBase<Vendas>
{
    public int Id { get; private set; }

    public decimal Valor { get; private set; }

    public DateTime Data { get; private set; }

    public int ClienteId { get; private set; }

    public int LivroId { get; private set; }

    public virtual Clientes Cliente { get; private set; } = null!;

    public virtual Livros Livro { get; private set; } = null!;

    protected Vendas() { }

    public Vendas(CadastroVendaModel modelo, decimal valor)
    {
        this.Valor = valor;
        this.Data = modelo.Data;
        this.ClienteId = modelo.ClienteId;
        this.LivroId = modelo.LivroId;
    }

    protected override void ValidateState() { }
}
