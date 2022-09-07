namespace Y.IssueTracker.Api.Infrastructure.QueryServices;

using Comments;
using Comments.Results;
using Microsoft.EntityFrameworkCore;
using Y.IssueTracker.Comments.Queries;

internal sealed class CommentQueryService : ICommentQueryService
{
    private readonly ApplicationDbContext applicationDbContext;

    public CommentQueryService(ApplicationDbContext applicationDbContext)
    {
        this.applicationDbContext = applicationDbContext;
    }

    public Task<CommentForViewResult[]> HandleAsync(GetCommentsForViewQuery query)
    {
        var userQuery = this.applicationDbContext
            .Users
            .AsNoTracking();

        return this.applicationDbContext
            .Comments
            .AsNoTracking()
            .Where(x => x.IssueId == query.IssueId)
            .Join(userQuery, x => x.AuthorUserId, x => x.Id, (x, y) => new { comment = x, user = y })
            .Select(x => new CommentForViewResult
            {
                Id = x.comment.Id,
                Text = x.comment.Text,
                AuthorUserId = x.user.Id,
                AuthorUserName = x.user.Name,
                CreatedOn = x.comment.CreatedOn
            })
            .ToArrayAsync();
    }
}
