namespace Livraria.Core.Domain.Ports;

public interface IGenericRepository<TEntity>
{
    void Add(TEntity entity);

    Task AddAsync(TEntity entity);

    void Disposes();

    TEntity? GetById(int id);

    Task<TEntity?> GetByIdAsync(int id);

    bool Commit();

    Task<bool> CommitAsync();

    void Update(TEntity entity);

    void Delete(TEntity entity);

    void DeleteRange(IEnumerable<TEntity> entity);
}
