namespace Y.IssueTracker.Issues.Results;

using Domain;

public sealed class IssueForListItemResult
{
    public Guid Id { get; init; }

    public string Name { get; init; }

    public string Project { get; init; }

    public string Category { get; init; }

    public string Priority { get; init; }

    public IssueStatus Status { get; init; }

    public string AssignedUserName { get; init; }

    public string AuthorUserName { get; init; }

    public DateTime CreatedOn { get; init; }
}
