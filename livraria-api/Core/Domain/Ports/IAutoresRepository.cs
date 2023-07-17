using Livraria.Core.Domain.Entities;
using Livraria.Core.Domain.Shared;

namespace Livraria.Core.Domain.Ports;

public interface IAutoresRepository : IGenericRepository<Autores>
{
    Task<Autores?> ObterPorIdAsync(int id);

    Task<Paginator<Autores>> ObterTodosAsync(int pageSize, int pageNumber);

    Task CadastrarAsync(Autores autores);

    Task AtualizarAsync(Autores autores);

    Task DeletarAsync(Autores autores);
}