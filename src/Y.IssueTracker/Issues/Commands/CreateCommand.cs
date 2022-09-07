namespace Y.IssueTracker.Issues.Commands;

public sealed class CreateCommand
{
    public string Name { get; init; }

    public string Description { get; init; }

    public Guid ProjectId { get; init; }

    public Guid CategoryId { get; init; }

    public Guid PriorityId { get; init; }

    public Guid AssignedUserId { get; init; }

    public Guid AuthorUserId { get; init; }
}
