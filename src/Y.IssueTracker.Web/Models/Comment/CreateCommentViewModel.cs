namespace Y.IssueTracker.Web.Models.Comment;

public sealed class CreateCommentViewModel
{
    public Guid IssueId { get; init; }

    public string Text { get; init; }

    public Guid AuthorUserId { get; init; }
}
