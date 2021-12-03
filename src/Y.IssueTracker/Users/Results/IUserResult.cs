namespace Y.IssueTracker.Users.Results;

using Domain;

public interface IUserResult
{
    Guid Id { get; }

    string Name { get; }

    Role Role { get; }

    bool IsActive { get; }

    bool IsDefault { get; }
}
