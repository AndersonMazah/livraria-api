using Livraria.Core.Domain.Entities;
using Livraria.Core.Domain.Ports;
using Livraria.Infra.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Livraria.Infra.Database.Repositories;

public class AvaliacaoRepository : GenericRepository<Avaliacao>, IAvaliacaoRepository
{
    public AvaliacaoRepository(LivrariaDbContext context) : base(context) { }

    public async Task<Avaliacao?> ObterPorIdAsync(int id)
    {
        return await DbSet.Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<List<Avaliacao>> ObterTodasAvaliacoesDoLivroAsync(int idLivro)
    {
        List<Avaliacao> lista = await DbSet
            .AsNoTracking()
            .Where(l => l.LivroId == idLivro)
            .OrderByDescending(x => x.Nota)
            .ToListAsync();
        return lista;
    }

    public async Task CadastrarAsync(Avaliacao avaliacao)
    {
        DbSet.Add(avaliacao);
        await Db.SaveChangesAsync();
    }

    public async Task AtualizarAsync(Avaliacao avaliacao)
    {
        DbSet.Update(avaliacao);
        await Db.SaveChangesAsync();
    }

    public async Task DeletarAsync(Avaliacao avaliacao)
    {
        DbSet.Remove(avaliacao);
        await Db.SaveChangesAsync();
    }

    public async Task<bool> VerificarSeLivroExisteAsync(int idLivro)
    {
        return await Db.Set<Livros>().AnyAsync(l => l.Id == idLivro);
    }
}
