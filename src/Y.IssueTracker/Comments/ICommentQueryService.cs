namespace Y.IssueTracker.Comments;

using Results;
using Y.IssueTracker.Comments.Queries;

public interface ICommentQueryService
{
    Task<CommentForViewResult[]> HandleAsync(GetCommentsForViewQuery query);
}
