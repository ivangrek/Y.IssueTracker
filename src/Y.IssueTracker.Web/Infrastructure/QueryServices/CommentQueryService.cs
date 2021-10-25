namespace Y.IssueTracker.Web.Infrastructure.QueryServices
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Comments;
    using Comments.Results;
    using Microsoft.EntityFrameworkCore;
    using Results;

    internal sealed class CommentQueryService : ICommentQueryService
    {
        private readonly ApplicationDbContext applicationDbContext;

        public CommentQueryService(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public Task<ICommentForViewResult[]> QueryCommentsForViewAsync(Guid issueId)
        {
            var userQuery = this.applicationDbContext
                .Users
                .AsNoTracking();

            return this.applicationDbContext
                .Comments
                .AsNoTracking()
                .Where(x => x.IssueId == issueId)
                .Join(userQuery, x => x.AuthorUserId, x => x.Id, (x, y) => new { comment = x, user = y })
                .Select(x => new CommentForViewResult
                {
                    Id = x.comment.Id,
                    Text = x.comment.Text,
                    AuthorUserId = x.user.Id,
                    AuthorUserName = x.user.Name,
                    CreatedOn = x.comment.CreatedOn
                })
                .Cast<ICommentForViewResult>()
                .ToArrayAsync();
        }
    }
}
