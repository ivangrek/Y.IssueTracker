namespace Y.IssueTracker.Issues.Commands;

using Domain;

public interface IUpdateCommand
{
    Guid Id { get; }

    string Name { get; }

    string Description { get; }

    Guid ProjectId { get; }

    Guid CategoryId { get; }

    Guid PriorityId { get; }

    IssueStatus Status { get; }

    Guid AssignedUserId { get; }
}
