namespace Y.IssueTracker.Web.Infrastructure.QueryServices.Results;

using Categories.Results;

internal sealed class CategoryResult : ICategoryResult
{
    public Guid Id { get; init; }

    public string Name { get; init; }

    public bool IsActive { get; init; }
}
