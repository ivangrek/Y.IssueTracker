namespace Y.IssueTracker.Comments.Commands;

public interface ICreateCommand
{
    Guid IssueId { get; }

    string Text { get; }

    Guid AuthorUserId { get; }
}
