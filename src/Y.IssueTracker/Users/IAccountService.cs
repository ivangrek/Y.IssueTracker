namespace Y.IssueTracker.Users;

using System;
using System.Threading.Tasks;
using Y.IssueTracker.Users.Domain;

public interface IAccountService
{
    Task SignInAsync(Guid userId, string name, Role role, bool rememberMe);

    Task SignOutAsync();

    Task SignOutAsync(Guid userId);
}
