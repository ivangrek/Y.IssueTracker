namespace Y.IssueTracker.Comments.Queries;

public sealed class GetCommentsForViewQuery
{
    public Guid IssueId { get; init; }
}
