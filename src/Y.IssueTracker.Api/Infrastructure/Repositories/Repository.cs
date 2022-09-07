namespace Y.IssueTracker.Api.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;

internal abstract class Repository<TEntity> : IRepository<TEntity>
    where TEntity : class, IEntity
{
    protected readonly ApplicationDbContext ApplicationDbContext;

    protected Repository(ApplicationDbContext applicationDbContext)
    {
        this.ApplicationDbContext = applicationDbContext;
    }

    public async Task AddAsync(TEntity entity)
    {
        _ = await this.ApplicationDbContext
            .AddAsync(entity);
    }

    public void Remove(TEntity entity)
    {
        this.ApplicationDbContext
            .Remove(entity);
    }

    public IQueryable<TEntity> QueryAll()
    {
        return this.ApplicationDbContext
            .Set<TEntity>();
    }

    public Task<TEntity> FindByIdAsync(Guid id)
    {
        return this.ApplicationDbContext
            .Set<TEntity>()
            .SingleOrDefaultAsync(x => x.Id == id);
    }
}
