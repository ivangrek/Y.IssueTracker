namespace Y.IssueTracker.Priorities.Commands;

public sealed class CreateCommand
{
    public string Name { get; init; }

    public int Weight { get; init; }
}
