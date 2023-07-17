using Livraria.Core.Domain.Entities;
using Livraria.Core.Domain.Ports;
using Livraria.Core.Domain.Shared;
using Livraria.Infra.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Livraria.Infra.Database.Repositories;

public class LivrosRepository : GenericRepository<Livros>, ILivrosRepository
{
    public LivrosRepository(LivrariaDbContext context) : base(context) { }

    public async Task<Livros?> ObterPorIdAsync(int id)
    {
        return await DbSet.Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Paginator<Livros>> ObterTodosAsync(int pageSize, int pageNumber, int idAutor)
    {
        int page = (pageNumber - 1) * pageSize;

        var query = DbSet.AsQueryable();
        query = query.AsNoTracking();
        if (idAutor > 0)
        {
            query = query.Where(v => v.AutorId == idAutor);
        }
        int total = await query.CountAsync();
        query = query.OrderBy(x => x.Nome);
        List<Livros> lista = await query.Skip(page).Take(pageSize).ToListAsync();
        Paginator<Livros> paginacao = new Paginator<Livros>(pageSize, pageNumber, total, lista);
        return paginacao;
    }

    public async Task CadastrarAsync(Livros livros)
    {
        DbSet.Add(livros);
        await Db.SaveChangesAsync();
    }

    public async Task AtualizarAsync(Livros livros)
    {
        DbSet.Update(livros);
        await Db.SaveChangesAsync();
    }

    public async Task DeletarAsync(Livros livros)
    {
        DbSet.Remove(livros);
        await Db.SaveChangesAsync();
    }

    public async Task<bool> VerificarSeExisteAlgumLivroDoAutorAsync(int idAutor)
    {
        return await DbSet.AnyAsync(l => l.AutorId == idAutor);
    }
}
