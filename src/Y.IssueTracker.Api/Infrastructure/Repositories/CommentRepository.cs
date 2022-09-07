namespace Y.IssueTracker.Api.Infrastructure.Repositories;

using Comments.Domain;

internal sealed class CommentRepository : Repository<Comment>, ICommentRepository
{
    public CommentRepository(ApplicationDbContext applicationDbContext)
        : base(applicationDbContext)
    {
    }
}
