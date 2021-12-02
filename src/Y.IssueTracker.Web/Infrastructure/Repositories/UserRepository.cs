namespace Y.IssueTracker.Web.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Users.Domain;

internal sealed class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext applicationDbContext)
        : base(applicationDbContext)
    {
    }

    public Task<User> FindByEmailAsync(string email)
    {
        return this.ApplicationDbContext
            .Set<User>()
            .SingleOrDefaultAsync(x => x.Email == email);
    }
}
