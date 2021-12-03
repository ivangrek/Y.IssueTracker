namespace Y.IssueTracker.Categories.Commands;

public interface IUpdateCommand
{
    Guid Id { get; }

    string Name { get; }
}
