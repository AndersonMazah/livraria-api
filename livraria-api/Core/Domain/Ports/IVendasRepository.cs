using Livraria.Core.Domain.Entities;
using Livraria.Core.Domain.Shared;

namespace Livraria.Core.Domain.Ports;

public interface IVendasRepository : IGenericRepository<Vendas>
{
    Task<Vendas?> ObterPorIdAsync(int id);

    Task<Paginator<Vendas>> ObterTodosAsync(int pageSize, int pageNumber, int idCliente, int idLivro, int idAutor);

    Task CadastrarAsync(Vendas vendas);

    Task AtualizarAsync(Vendas vendas);

    Task DeletarAsync(Vendas vendas);

    Task<bool> VerificarSeExisteAlgumaVendaParaOClienteAsync(int idCliente);

    Task<bool> VerificarSeExisteAlgumaVendaDoLivroAsync(int idLivro);

}