namespace Y.IssueTracker
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IRepository<TEntity>
        where TEntity : class, IEntity
    {
        void Add(TEntity entity);

        void Remove(TEntity entity);

        IQueryable<TEntity> QueryAll();

        Task<TEntity> QueryByIdAsync(Guid id);
    }
}
