namespace Y.IssueTracker.Web.Infrastructure.Repositories;

using Categories.Domain;

internal sealed class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext applicationDbContext)
        : base(applicationDbContext)
    {
    }
}
