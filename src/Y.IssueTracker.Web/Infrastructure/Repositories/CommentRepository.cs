namespace Y.IssueTracker.Web.Infrastructure.Repositories;

using Comments.Domain;

internal sealed class CommentRepository : Repository<Comment>, ICommentRepository
{
    public CommentRepository(ApplicationDbContext applicationDbContext)
        : base(applicationDbContext)
    {
    }
}
