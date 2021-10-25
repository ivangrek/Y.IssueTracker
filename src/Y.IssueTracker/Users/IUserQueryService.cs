namespace Y.IssueTracker.Users
{
    using System;
    using System.Threading.Tasks;
    using Results;

    public interface IUserQueryService
    {
        Task<IUserResult[]> QueryAllAsync();

        Task<IUserResult> QueryByIdAsync(Guid id);
    }
}
