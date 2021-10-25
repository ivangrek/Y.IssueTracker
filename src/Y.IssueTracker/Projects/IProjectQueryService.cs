namespace Y.IssueTracker.Projects
{
    using System;
    using System.Threading.Tasks;
    using Results;

    public interface IProjectQueryService
    {
        Task<IProjectResult[]> QueryAllAsync();

        Task<IProjectResult> QueryByIdAsync(Guid id);
    }
}
