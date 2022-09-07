namespace Y.IssueTracker.Api.Infrastructure.QueryServices;

using Microsoft.EntityFrameworkCore;
using Users;
using Users.Results;
using Y.IssueTracker.Users.Queries;

internal sealed class UserQueryService : IUserQueryService
{
    private readonly ApplicationDbContext applicationDbContext;

    public UserQueryService(ApplicationDbContext applicationDbContext)
    {
        this.applicationDbContext = applicationDbContext;
    }

    public Task<UserResult[]> HandleAsync(GetAllQuery query)
    {
        return this.applicationDbContext
            .Users
            .AsNoTracking()
            .Skip((query.Page - 1) * query.PageCount)
            .Take(query.PageCount)
            .Select(x => new UserResult
            {
                Id = x.Id,
                Name = x.Name,
                Role = x.Role,
                IsActive = x.IsActive,
                IsDefault = x.IsDefault
            })
            .ToArrayAsync();
    }

    public Task<UserResult> HandleAsync(GetByIdQuery query)
    {
        return this.applicationDbContext
            .Users
            .AsNoTracking()
            .Where(x => x.Id == query.Id)
            .Select(x => new UserResult
            {
                Id = x.Id,
                Name = x.Name,
                Role = x.Role,
                IsActive = x.IsActive,
                IsDefault = x.IsDefault
            })
            .SingleOrDefaultAsync();
    }

    public Task<UserResult> QueryByCredentialsAsync(string email, string password)
    {
        return this.applicationDbContext
            .Users
            .AsNoTracking()
            .Where(x => x.Email == email)
            .Where(x => x.Password == password)
            .Where(x => x.IsActive)
            .Select(x => new UserResult
            {
                Id = x.Id,
                Name = x.Name,
                Role = x.Role,
                IsActive = x.IsActive,
                IsDefault = x.IsDefault
            })
            .SingleOrDefaultAsync();
    }

    public Task<bool> QueryCheckUserExistsAsync(string email)
    {
        return this.applicationDbContext
            .Users
            .AsNoTracking()
            .Where(x => x.Email == email)
            .AnyAsync();
    }
}
