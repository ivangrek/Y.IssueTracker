namespace Y.IssueTracker.Web.Infrastructure.QueryServices;

using Microsoft.EntityFrameworkCore;
using Priorities;
using Priorities.Results;
using Results;

internal sealed class PriorityQueryService : IPriorityQueryService
{
    private readonly ApplicationDbContext applicationDbContext;

    public PriorityQueryService(ApplicationDbContext applicationDbContext)
    {
        this.applicationDbContext = applicationDbContext;
    }

    public Task<IPriorityResult[]> QueryAllAsync()
    {
        return this.applicationDbContext
            .Priorities
            .AsNoTracking()
            .Select(x => new PriorityResult
            {
                Id = x.Id,
                Name = x.Name,
                Weight = x.Weight,
                IsActive = x.IsActive
            })
            .Cast<IPriorityResult>()
            .ToArrayAsync();
    }

    public Task<IPriorityResult> QueryByIdAsync(Guid id)
    {
        return this.applicationDbContext
            .Priorities
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => new PriorityResult
            {
                Id = x.Id,
                Name = x.Name,
                Weight = x.Weight,
                IsActive = x.IsActive
            })
            .Cast<IPriorityResult>()
            .SingleOrDefaultAsync();
    }
}
