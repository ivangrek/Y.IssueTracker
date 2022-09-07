namespace Y.IssueTracker.Priorities.Results;

public sealed class PriorityResult
{
    public Guid Id { get; init; }

    public string Name { get; init; }

    public int Weight { get; init; }

    public bool IsActive { get; init; }
}
