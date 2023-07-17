using Livraria.Core.Application.Dtos;
using Livraria.Core.Domain.Models;
using Livraria.Core.Domain.Shared;

namespace Livraria.Core.Application.Ports;

public interface IVendasApplication
{
    Task<Response<VendasDto>> ObterPorIdAsync(int id);

    Task<Response<Paginator<VendasDto>>> ObterTodosAsync(int pageSize, int pageNumber, int idCliente, int idLivro, int idAutor);

    Task<Response<VendasDto>> CadastrarAsync(CadastroVendaModel modelo);

}
