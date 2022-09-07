namespace Y.IssueTracker.Api.Infrastructure.QueryServices;

using Categories;
using Microsoft.EntityFrameworkCore;
using Y.IssueTracker.Categories.Queries;
using Y.IssueTracker.Categories.Results;

internal sealed class CategoryQueryService : ICategoryQueryService
{
    private readonly ApplicationDbContext applicationDbContext;

    public CategoryQueryService(ApplicationDbContext applicationDbContext)
    {
        this.applicationDbContext = applicationDbContext;
    }

    public async Task<CategoryResult[]> HandleAsync(GetAllQuery query)
    {
        return await this.applicationDbContext
            .Categories
            .AsNoTracking()
            .Skip((query.Page - 1) * query.PageCount)
            .Take(query.PageCount)
            .Select(x => new CategoryResult
            {
                Id = x.Id,
                Name = x.Name,
                IsActive = x.IsActive
            })
            .ToArrayAsync();
    }

    public Task<CategoryResult> HandleAsync(GetByIdQuery query)
    {
        return this.applicationDbContext
            .Categories
            .AsNoTracking()
            .Where(x => x.Id == query.Id)
            .Select(x => new CategoryResult
            {
                Id = x.Id,
                Name = x.Name,
                IsActive = x.IsActive
            })
            .SingleOrDefaultAsync();
    }
}
