namespace Y.IssueTracker.Projects.Commands;

public sealed class UpdateCommand
{
    public Guid Id { get; init; }

    public string Name { get; init; }
}
