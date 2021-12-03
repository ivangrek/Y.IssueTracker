namespace Y.IssueTracker.Comments.Results;

public interface ICommentForViewResult
{
    Guid Id { get; }

    string Text { get; }

    Guid AuthorUserId { get; }

    string AuthorUserName { get; }

    DateTime CreatedOn { get; }
}
