using Livraria.Core.Domain.Entities;
using Livraria.Core.Domain.Ports;
using Livraria.Core.Domain.Shared;
using Livraria.Infra.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Livraria.Infra.Database.Repositories;

public class AutoresRepository : GenericRepository<Autores>, IAutoresRepository
{
    public AutoresRepository(LivrariaDbContext context) : base(context) { }

    public async Task<Autores?> ObterPorIdAsync(int id)
    {
        return await DbSet.Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Paginator<Autores>> ObterTodosAsync(int pageSize, int pageNumber)
    {
        int page = (pageNumber - 1) * pageSize;
        List<Autores> lista = await DbSet
            .AsNoTracking()
            .OrderBy(x => x.Nome)
            .Skip(page)
            .Take(pageSize)
            .ToListAsync();

        int total = await DbSet.CountAsync();

        Paginator<Autores> paginacao = new Paginator<Autores>(pageSize, pageNumber, total, lista);
        return paginacao;
    }

    public async Task CadastrarAsync(Autores autores)
    {
        DbSet.Add(autores);
        await Db.SaveChangesAsync();
    }

    public async Task AtualizarAsync(Autores autores)
    {
        DbSet.Update(autores);
        await Db.SaveChangesAsync();
    }

    public async Task DeletarAsync(Autores autores)
    {
        DbSet.Remove(autores);
        await Db.SaveChangesAsync();
    }
}
