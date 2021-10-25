namespace Y.IssueTracker.Issues.Results
{
    using System;
    using Domain;

    public interface IIssueResult
    {
        Guid Id { get; }

        string Name { get; }

        string Description { get; }

        Guid ProjectId { get; }

        Guid CategoryId { get; }

        Guid PriorityId { get; }

        IssueStatus Status { get; }

        Guid AssignedUserId { get; }

        Guid AuthorUserId { get; }

        DateTime CreatedOn { get; }
    }
}
