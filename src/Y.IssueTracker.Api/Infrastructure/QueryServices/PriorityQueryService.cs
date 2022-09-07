namespace Y.IssueTracker.Api.Infrastructure.QueryServices;

using Microsoft.EntityFrameworkCore;
using Priorities;
using Priorities.Results;
using Y.IssueTracker.Priorities.Queries;

internal sealed class PriorityQueryService : IPriorityQueryService
{
    private readonly ApplicationDbContext applicationDbContext;

    public PriorityQueryService(ApplicationDbContext applicationDbContext)
    {
        this.applicationDbContext = applicationDbContext;
    }

    public Task<PriorityResult[]> HandleAsync(GetAllQuery query)
    {
        return this.applicationDbContext
            .Priorities
            .AsNoTracking()
            .Skip((query.Page - 1) * query.PageCount)
            .Take(query.PageCount)
            .OrderBy(x => x.Weight)
            .Select(x => new PriorityResult
            {
                Id = x.Id,
                Name = x.Name,
                Weight = x.Weight,
                IsActive = x.IsActive
            })
            .ToArrayAsync();
    }

    public Task<PriorityResult> HandleAsync(GetByIdQuery query)
    {
        return this.applicationDbContext
            .Priorities
            .AsNoTracking()
            .Where(x => x.Id == query.Id)
            .Select(x => new PriorityResult
            {
                Id = x.Id,
                Name = x.Name,
                Weight = x.Weight,
                IsActive = x.IsActive
            })
            .SingleOrDefaultAsync();
    }
}
