using Livraria.Core.Domain.Entities;
using Livraria.Core.Domain.Ports;
using Livraria.Core.Domain.Shared;
using Livraria.Infra.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Livraria.Infra.Database.Repositories;

public class VendasRepository : GenericRepository<Vendas>, IVendasRepository
{
    public VendasRepository(LivrariaDbContext context) : base(context) { }

    public async Task<Vendas?> ObterPorIdAsync(int id)
    {
        return await DbSet.Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Paginator<Vendas>> ObterTodosAsync(int pageSize, int pageNumber, int idCliente, int idLivro, int idAutor)
    {
        int page = (pageNumber - 1) * pageSize;

        var query = DbSet.AsQueryable();
        query = query.AsNoTracking();
        if (idCliente > 0)
        {
            query = query.Where(v => v.ClienteId == idCliente);
        }
        if (idLivro > 0)
        {
            query = query.Where(v => v.LivroId == idLivro);
        }
        if (idAutor > 0)
        {
            query = query.Where(v => v.Livro.AutorId == idAutor);
        }
        int total = await query.CountAsync();
        query = query.OrderBy(x => x.Data);
        List<Vendas> lista = await query.Skip(page).Take(pageSize).ToListAsync();
        Paginator<Vendas> paginacao = new Paginator<Vendas>(pageSize, pageNumber, total, lista);
        return paginacao;
    }

    public async Task CadastrarAsync(Vendas vendas)
    {
        DbSet.Add(vendas);
        await Db.SaveChangesAsync();
    }

    public async Task AtualizarAsync(Vendas vendas)
    {
        DbSet.Update(vendas);
        await Db.SaveChangesAsync();
    }

    public async Task DeletarAsync(Vendas vendas)
    {
        DbSet.Remove(vendas);
        await Db.SaveChangesAsync();
    }

    public async Task<bool> VerificarSeExisteAlgumaVendaParaOClienteAsync(int idCliente)
    {
        return await DbSet.AnyAsync(v => v.ClienteId == idCliente);
    }

    public async Task<bool> VerificarSeExisteAlgumaVendaDoLivroAsync(int idLivro)
    {
        return await DbSet.AnyAsync(v => v.LivroId == idLivro);
    }
}
