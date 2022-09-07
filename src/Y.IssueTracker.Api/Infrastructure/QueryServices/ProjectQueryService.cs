namespace Y.IssueTracker.Api.Infrastructure.QueryServices;

using Microsoft.EntityFrameworkCore;
using Projects;
using Projects.Results;
using Y.IssueTracker.Projects.Queries;

internal sealed class ProjectQueryService : IProjectQueryService
{
    private readonly ApplicationDbContext applicationDbContext;

    public ProjectQueryService(ApplicationDbContext applicationDbContext)
    {
        this.applicationDbContext = applicationDbContext;
    }

    public Task<ProjectResult[]> HandleAsync(GetAllQuery query)
    {
        return this.applicationDbContext
            .Projects
            .AsNoTracking()
            .Skip((query.Page - 1) * query.PageCount)
            .Take(query.PageCount)
            .Select(x => new ProjectResult
            {
                Id = x.Id,
                Name = x.Name,
                IsActive = x.IsActive
            })
            .ToArrayAsync();
    }

    public Task<ProjectResult> HandleAsync(GetByIdQuery query)
    {
        return this.applicationDbContext
            .Projects
            .AsNoTracking()
            .Where(x => x.Id == query.Id)
            .Select(x => new ProjectResult
            {
                Id = x.Id,
                Name = x.Name,
                IsActive = x.IsActive
            })
            .SingleOrDefaultAsync();
    }
}
