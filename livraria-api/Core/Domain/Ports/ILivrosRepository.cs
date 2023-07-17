using Livraria.Core.Domain.Entities;
using Livraria.Core.Domain.Shared;

namespace Livraria.Core.Domain.Ports;

public interface ILivrosRepository : IGenericRepository<Livros>
{
    Task<Livros?> ObterPorIdAsync(int id);

    Task<Paginator<Livros>> ObterTodosAsync(int pageSize, int pageNumber, int idAutor);

    Task CadastrarAsync(Livros livros);

    Task AtualizarAsync(Livros livros);

    Task DeletarAsync(Livros livros);

    Task<bool> VerificarSeExisteAlgumLivroDoAutorAsync(int idAutor);
}