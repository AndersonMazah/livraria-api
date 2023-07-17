using Livraria.Core.Domain.Entities;

namespace Livraria.Core.Domain.Ports;

public interface IAvaliacaoRepository : IGenericRepository<Avaliacao>
{
    Task<Avaliacao?> ObterPorIdAsync(int id);

    Task<List<Avaliacao>> ObterTodasAvaliacoesDoLivroAsync(int idLivro);

    Task CadastrarAsync(Avaliacao avaliacao);

    Task AtualizarAsync(Avaliacao avaliacao);

    Task DeletarAsync(Avaliacao avaliacao);

    Task<bool> VerificarSeLivroExisteAsync(int idLivro);
}