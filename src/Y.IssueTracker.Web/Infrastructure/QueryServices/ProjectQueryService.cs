namespace Y.IssueTracker.Web.Infrastructure.QueryServices
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Projects;
    using Projects.Results;
    using Results;

    internal sealed class ProjectQueryService : IProjectQueryService
    {
        private readonly ApplicationDbContext applicationDbContext;

        public ProjectQueryService(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public Task<IProjectResult[]> QueryAllAsync()
        {
            return this.applicationDbContext
                .Projects
                .AsNoTracking()
                .Select(x => new ProjectResult
                {
                    Id = x.Id,
                    Name = x.Name,
                    IsActive = x.IsActive
                })
                .Cast<IProjectResult>()
                .ToArrayAsync();
        }

        public Task<IProjectResult> QueryByIdAsync(Guid id)
        {
            return this.applicationDbContext
                .Projects
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new ProjectResult
                {
                    Id = x.Id,
                    Name = x.Name,
                    IsActive = x.IsActive
                })
                .Cast<IProjectResult>()
                .SingleOrDefaultAsync();
        }
    }
}
