namespace Y.IssueTracker.Web.Infrastructure.QueryServices;

using Categories;
using Categories.Results;
using Microsoft.EntityFrameworkCore;
using Results;

internal sealed class CategoryQueryService : ICategoryQueryService
{
    private readonly ApplicationDbContext applicationDbContext;

    public CategoryQueryService(ApplicationDbContext applicationDbContext)
    {
        this.applicationDbContext = applicationDbContext;
    }

    public Task<ICategoryResult[]> QueryAllAsync()
    {
        return this.applicationDbContext
            .Categories
            .AsNoTracking()
            .Select(x => new CategoryResult
            {
                Id = x.Id,
                Name = x.Name,
                IsActive = x.IsActive
            })
            .Cast<ICategoryResult>()
            .ToArrayAsync();
    }

    public Task<ICategoryResult> QueryByIdAsync(Guid id)
    {
        return this.applicationDbContext
            .Categories
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => new CategoryResult
            {
                Id = x.Id,
                Name = x.Name,
                IsActive = x.IsActive
            })
            .Cast<ICategoryResult>()
            .SingleOrDefaultAsync();
    }
}
