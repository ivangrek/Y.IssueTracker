namespace Y.IssueTracker.Categories;

using Results;
using Y.IssueTracker.Categories.Queries;

public interface ICategoryQueryService
{
    Task<CategoryResult[]> HandleAsync(GetAllQuery query);

    Task<CategoryResult> HandleAsync(GetByIdQuery query);
}
