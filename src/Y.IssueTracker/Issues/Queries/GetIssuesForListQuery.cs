namespace Y.IssueTracker.Issues.Queries;

public sealed class GetIssuesForListQuery
{
    public int Page { get; init; }

    public int PageCount { get; init; }
}
