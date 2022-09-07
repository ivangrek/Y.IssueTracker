namespace Y.IssueTracker.Comments.Commands;

public sealed class CreateCommand
{
    public Guid IssueId { get; init; }

    public string Text { get; init; }

    public Guid AuthorUserId { get; init; }
}
