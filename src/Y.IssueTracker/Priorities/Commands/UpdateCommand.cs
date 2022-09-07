namespace Y.IssueTracker.Priorities.Commands;

public sealed class UpdateCommand
{
    public Guid Id { get; init; }

    public string Name { get; init; }

    public int Weight { get; init; }
}
