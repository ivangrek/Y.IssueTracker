namespace Y.IssueTracker.Users.Domain;

using System.Threading.Tasks;

public interface IUserRepository : IRepository<User>
{
    Task<User> FindByEmailAsync(string email);
}
