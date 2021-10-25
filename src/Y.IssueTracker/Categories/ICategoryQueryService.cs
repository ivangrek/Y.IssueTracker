namespace Y.IssueTracker.Categories
{
    using System;
    using System.Threading.Tasks;
    using Results;

    public interface ICategoryQueryService
    {
        Task<ICategoryResult[]> QueryAllAsync();

        Task<ICategoryResult> QueryByIdAsync(Guid id);
    }
}
