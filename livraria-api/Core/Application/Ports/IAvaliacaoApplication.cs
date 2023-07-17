using Livraria.Core.Application.Dtos;
using Livraria.Core.Domain.Models;
using Livraria.Core.Domain.Shared;

namespace Livraria.Core.Application.Ports;

public interface IAvaliacaoApplication
{
    Task<Response<AvaliacaoDto>> ObterPorIdAsync(int id);

    Task<Response<List<AvaliacaoDto>>> ObterTodasAvaliacoesDoLivroAsync(int idLivro);

    Task<Response<AvaliacaoDto>> CadastrarAsync(CadastroAvaliacaoModel modelo);

    Task<Response<AvaliacaoDto>> AtualizarAsync(AtualizaAvaliacaoModel modelo);

    Task<Response<AvaliacaoDto>> DeletarAsync(int id);
}
