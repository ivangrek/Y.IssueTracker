namespace Y.IssueTracker.Api.Infrastructure.Repositories;

using Priorities.Domain;

internal sealed class PriorityRepository : Repository<Priority>, IPriorityRepository
{
    public PriorityRepository(ApplicationDbContext applicationDbContext)
        : base(applicationDbContext)
    {
    }
}
