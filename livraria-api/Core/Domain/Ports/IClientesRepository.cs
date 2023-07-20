using Livraria.Core.Domain.Entities;
using Livraria.Core.Domain.Shared;

namespace Livraria.Core.Domain.Ports;

public interface IClientesRepository : IGenericRepository<Clientes>
{
    Task<Clientes?> ObterPorIdAsync(int id);

    Task<Paginator<Clientes>> ObterTodosAsync(int pageSize, int pageNumber);

    Task CadastrarAsync(Clientes clientes);

    Task AtualizarAsync(Clientes clientes);

    Task DeletarAsync(Clientes clientes);

    Task<bool> VerificarSeClienteExiste(int idCliente);

    Task<bool> VerificarLoginDeClienteAsync(string username, string password);
}