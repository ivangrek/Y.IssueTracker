namespace Y.IssueTracker.Comments;

using System;
using System.Threading.Tasks;
using Results;

public interface ICommentQueryService
{
    Task<ICommentForViewResult[]> QueryCommentsForViewAsync(Guid issueId);
}
