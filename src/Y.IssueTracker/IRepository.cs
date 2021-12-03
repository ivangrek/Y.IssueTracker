namespace Y.IssueTracker;

public interface IRepository<TEntity>
    where TEntity : class, IEntity
{
    Task AddAsync(TEntity entity);

    void Remove(TEntity entity);

    IQueryable<TEntity> QueryAll();

    Task<TEntity> FindByIdAsync(Guid id);
}
