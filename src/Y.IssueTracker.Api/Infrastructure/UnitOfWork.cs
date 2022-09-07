namespace Y.IssueTracker.Api.Infrastructure;

using System.Threading.Tasks;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext applicationDbContext;

    public UnitOfWork(ApplicationDbContext applicationDbContext)
    {
        this.applicationDbContext = applicationDbContext;
    }

    public Task CommitAsync()
    {
        return this.applicationDbContext
            .SaveChangesAsync();
    }
}
