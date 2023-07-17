using Livraria.Core.Domain.Entities;
using Livraria.Core.Domain.Ports;
using Livraria.Core.Domain.Shared;
using Livraria.Infra.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Livraria.Infra.Database.Repositories;

public class ClientesRepository : GenericRepository<Clientes>, IClientesRepository
{
    public ClientesRepository(LivrariaDbContext context) : base(context) { }

    public async Task<Clientes?> ObterPorIdAsync(int id)
    {
        return await DbSet.Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Paginator<Clientes>> ObterTodosAsync(int pageSize, int pageNumber)
    {
        int page = (pageNumber - 1) * pageSize;
        List<Clientes> lista = await DbSet
            .AsNoTracking()
            .OrderBy(x => x.Nome)
            .Skip(page)
            .Take(pageSize)
            .ToListAsync();

        int total = await DbSet.CountAsync();

        Paginator<Clientes> paginacao = new Paginator<Clientes>(pageSize, pageNumber, total, lista);
        return paginacao;
    }

    public async Task CadastrarAsync(Clientes clientes)
    {
        DbSet.Add(clientes);
        await Db.SaveChangesAsync();
    }

    public async Task AtualizarAsync(Clientes clientes)
    {
        DbSet.Update(clientes);
        await Db.SaveChangesAsync();
    }

    public async Task DeletarAsync(Clientes clientes)
    {
        DbSet.Remove(clientes);
        await Db.SaveChangesAsync();
    }

    public async Task<bool> VerificarSeClienteExiste(int idCliente)
    {
        return await DbSet.AnyAsync(c => c.Id == idCliente);
    }
}
