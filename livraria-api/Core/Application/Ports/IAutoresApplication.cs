using Livraria.Core.Application.Dtos;
using Livraria.Core.Domain.Models;
using Livraria.Core.Domain.Shared;

namespace Livraria.Core.Application.Ports;

public interface IAutoresApplication
{
    Task<Response<AutoresDto>> ObterPorIdAsync(int id);

    Task<Response<Paginator<AutoresDto>>> ObterTodosAsync(int pageSize, int pageNumber);

    Task<Response<AutoresDto>> CadastrarAsync(CadastroAutorModel modelo);

    Task<Response<AutoresDto>> AtualizarAsync(AtualizaAutorModel modelo);

    Task<Response<AutoresDto>> DeletarAsync(int id);
}
