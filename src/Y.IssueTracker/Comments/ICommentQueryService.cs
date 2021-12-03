namespace Y.IssueTracker.Comments;

using Results;

public interface ICommentQueryService
{
    Task<ICommentForViewResult[]> QueryCommentsForViewAsync(Guid issueId);
}
