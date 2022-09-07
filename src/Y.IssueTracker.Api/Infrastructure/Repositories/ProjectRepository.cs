namespace Y.IssueTracker.Api.Infrastructure.Repositories;

using Projects.Domain;

internal sealed class ProjectRepository : Repository<Project>, IProjectRepository
{
    public ProjectRepository(ApplicationDbContext applicationDbContext)
        : base(applicationDbContext)
    {
    }
}
