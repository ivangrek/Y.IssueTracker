namespace Y.IssueTracker.Issues.Results;

using System;
using Domain;

public interface IIssueForViewResult
{
    Guid Id { get; }

    string Name { get; }

    string Description { get; }

    string ProjectName { get; }

    string CategoryName { get; }

    string PriorityName { get; }

    IssueStatus Status { get; }

    Guid AssignedUserId { get; }

    string AssignedUserName { get; }

    Guid AuthorUserId { get; }

    string AuthorUserName { get; }

    DateTime CreatedOn { get; }
}
