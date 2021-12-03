namespace Y.IssueTracker.Users.Commands;

using Domain;

public interface IUpdateCommand
{
    Guid Id { get; }

    string Name { get; }

    Role Role { get; }
}
