using Microsoft.EntityFrameworkCore;

namespace Livraria.Infra.Database.Context;

public class AbstractContext : DbContext
{
    public bool Commit() => SaveChanges() > 0;

    public async Task<bool> CommitAsync() => await SaveChangesAsync().ConfigureAwait(true) > 0;
}
