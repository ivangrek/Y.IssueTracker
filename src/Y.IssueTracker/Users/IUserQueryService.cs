namespace Y.IssueTracker.Users;

using Results;
using Y.IssueTracker.Users.Queries;

public interface IUserQueryService
{
    Task<UserResult[]> HandleAsync(GetAllQuery query);

    Task<UserResult> HandleAsync(GetByIdQuery query);

    Task<UserResult> QueryByCredentialsAsync(string email, string password);

    Task<bool> QueryCheckUserExistsAsync(string email);
}
