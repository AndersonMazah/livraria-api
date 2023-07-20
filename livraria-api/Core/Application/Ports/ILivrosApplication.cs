using Livraria.Core.Application.Dtos;
using Livraria.Core.Domain.Models;
using Livraria.Core.Domain.Shared;

namespace Livraria.Core.Application.Ports;

public interface ILivrosApplication
{
    Task<Response<LivrosDetalheDto>> ObterPorIdAsync(int id);

    Task<Response<Paginator<LivrosDto>>> ObterTodosAsync(int pageSize, int pageNumber, int idAutor);

    Task<Response<LivrosDto>> CadastrarAsync(CadastroLivroModel modelo);

    Task<Response<LivrosDto>> AtualizarAsync(AtualizaLivroModel modelo);

    Task<Response<LivrosDto>> DeletarAsync(int id);

    Task<Response<InfoLivroDto>> SalvarInfoLivroAsync(InfoLivroModel modelo);

    Task<Response<InfoLivroDto>> ObterInfoPorIdAsync(int id);
}
