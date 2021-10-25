namespace Y.IssueTracker.Web.Infrastructure.Repositories
{
    using Users.Domain;

    internal sealed class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        {
        }
    }
}
