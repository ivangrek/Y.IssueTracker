namespace Y.IssueTracker.Categories;

using Results;

public interface ICategoryQueryService
{
    Task<ICategoryResult[]> QueryAllAsync();

    Task<ICategoryResult> QueryByIdAsync(Guid id);
}
