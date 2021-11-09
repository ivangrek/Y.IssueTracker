namespace Y.IssueTracker.Users
{
    using System;
    using System.Threading.Tasks;

    public interface IAccountService
    {
        Task SignOutAsync(Guid userId);
    }
}
