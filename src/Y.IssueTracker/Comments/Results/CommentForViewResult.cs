namespace Y.IssueTracker.Comments.Results;

public sealed class CommentForViewResult
{
    public Guid Id { get; init; }

    public string Text { get; init; }

    public Guid AuthorUserId { get; init; }

    public string AuthorUserName { get; init; }

    public DateTime CreatedOn { get; init; }
}
