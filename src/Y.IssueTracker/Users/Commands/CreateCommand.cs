namespace Y.IssueTracker.Users.Commands;

using Domain;

public sealed class CreateCommand
{
    public string Name { get; init; }

    public Role Role { get; init; }
}
