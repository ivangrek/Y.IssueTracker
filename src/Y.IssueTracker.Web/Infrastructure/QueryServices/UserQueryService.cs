namespace Y.IssueTracker.Web.Infrastructure.QueryServices
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Results;
    using Users;
    using Users.Results;

    internal sealed class UserQueryService : IUserQueryService
    {
        private readonly ApplicationDbContext applicationDbContext;

        public UserQueryService(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public Task<IUserResult[]> QueryAllAsync()
        {
            return this.applicationDbContext
                .Users
                .AsNoTracking()
                .Select(x => new UserResult
                {
                    Id = x.Id,
                    Name = x.Name,
                    Role = x.Role,
                    IsActive = x.IsActive
                })
                .Cast<IUserResult>()
                .ToArrayAsync();
        }

        public Task<IUserResult> QueryByIdAsync(Guid id)
        {
            return this.applicationDbContext
                .Users
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new UserResult
                {
                    Id = x.Id,
                    Name = x.Name,
                    Role = x.Role,
                    IsActive = x.IsActive
                })
                .Cast<IUserResult>()
                .SingleOrDefaultAsync();
        }
    }
}
