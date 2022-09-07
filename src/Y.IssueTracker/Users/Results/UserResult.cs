namespace Y.IssueTracker.Users.Results;

using Domain;

public sealed class UserResult
{
    public Guid Id { get; init; }

    public string Name { get; init; }

    public Role Role { get; init; }

    public bool IsActive { get; init; }

    public bool IsDefault { get; init; }
}
