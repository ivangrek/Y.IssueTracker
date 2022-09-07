namespace Y.IssueTracker.Projects.Results;

public sealed class ProjectResult
{
    public Guid Id { get; init; }

    public string Name { get; init; }

    public bool IsActive { get; init; }
}
