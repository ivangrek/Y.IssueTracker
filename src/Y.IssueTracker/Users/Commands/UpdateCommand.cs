namespace Y.IssueTracker.Users.Commands;

using Domain;

public sealed class UpdateCommand
{
    public Guid Id { get; init; }

    public string Name { get; init; }

    public Role Role { get; init; }
}
