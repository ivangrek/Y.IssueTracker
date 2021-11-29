namespace Y.IssueTracker.Issues.Commands;

using System;

public interface ICreateCommand
{
    string Name { get; }

    string Description { get; }

    Guid ProjectId { get; }

    Guid CategoryId { get; }

    Guid PriorityId { get; }

    Guid AssignedUserId { get; }

    Guid AuthorUserId { get; }
}
