using Livraria.Core.Application.Dtos;
using Livraria.Core.Domain.Models;
using Livraria.Core.Domain.Shared;

namespace Livraria.Core.Application.Ports;

public interface IClientesApplication
{
    Task<Response<ClientesDto>> ObterPorIdAsync(int id);

    Task<Response<Paginator<ClientesDto>>> ObterTodosAsync(int pageSize, int pageNumber);

    Task<Response<ClientesDto>> CadastrarAsync(CadastroClienteModel modelo);

    Task<Response<ClientesDto>> AtualizarAsync(AtualizaClienteModel modelo);

    Task<Response<ClientesDto>> DeletarAsync(int id);
}
