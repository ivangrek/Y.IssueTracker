namespace Y.IssueTracker.Issues.Commands;

using Domain;

public sealed class UpdateCommand
{
    public Guid Id { get; init; }

    public string Name { get; init; }

    public string Description { get; init; }

    public Guid ProjectId { get; init; }

    public Guid CategoryId { get; init; }

    public Guid PriorityId { get; init; }

    public IssueStatus Status { get; init; }

    public Guid AssignedUserId { get; init; }
}
