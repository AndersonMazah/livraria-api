using Livraria.Core.Domain.Ports;
using Microsoft.EntityFrameworkCore;

namespace Livraria.Infra.Database.Context;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    protected AbstractContext Db { get; private set; }

    protected DbSet<TEntity> DbSet { get; }

    public GenericRepository(AbstractContext context)
    {
        Db = context ?? throw new ArgumentException("Falha ao inicializar o repositorio. Contexto inválido.");
        DbSet = Db.Set<TEntity>();
    }

    public void Disposes()
    {
        Db.Dispose();
    }

    public virtual void Add(TEntity entity)
    {
        Db.Entry(entity).State = EntityState.Added;
        DbSet.Add(entity);
    }

    public async virtual Task AddAsync(TEntity entity)
    {
        Db.Entry(entity).State = EntityState.Added;
        await Db.AddAsync(entity);
    }

    public virtual TEntity? GetById(int id)
    {
        return DbSet.Find(id);
    }

    public async virtual Task<TEntity?> GetByIdAsync(int id)
    {
        return await DbSet.FindAsync(id);
    }

    public bool Commit()
    {
        return Db.Commit();
    }

    public async Task<bool> CommitAsync()
    {
        return await Db.CommitAsync();
    }

    public void Update(TEntity entity)
    {
        Db.Entry(entity).State = EntityState.Modified;
        Db.Update(entity);
    }

    public void Delete(TEntity entity)
    {
        Db.Entry(entity).State = EntityState.Deleted;
        DbSet.Remove(entity);
    }

    public void DeleteRange(IEnumerable<TEntity> entity)
    {
        Db.Entry(entity).State = EntityState.Deleted;
        DbSet.RemoveRange(entity);
    }
}
