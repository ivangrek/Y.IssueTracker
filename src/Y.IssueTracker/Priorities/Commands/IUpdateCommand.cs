namespace Y.IssueTracker.Priorities.Commands;

public interface IUpdateCommand
{
    Guid Id { get; }

    string Name { get; }

    int Weight { get; }
}
