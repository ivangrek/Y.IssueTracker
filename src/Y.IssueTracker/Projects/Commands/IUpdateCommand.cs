namespace Y.IssueTracker.Projects.Commands;

public interface IUpdateCommand
{
    Guid Id { get; }

    string Name { get; }
}
