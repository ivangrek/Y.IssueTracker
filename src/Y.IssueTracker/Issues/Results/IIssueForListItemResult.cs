namespace Y.IssueTracker.Issues.Results;

using System;
using Domain;

public interface IIssueForListItemResult
{
    Guid Id { get; }

    string Name { get; }

    string Project { get; }

    string Category { get; }

    string Priority { get; }

    IssueStatus Status { get; }

    string AssignedUserName { get; }

    string AuthorUserName { get; }

    DateTime CreatedOn { get; }
}
