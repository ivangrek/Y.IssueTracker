namespace Y.IssueTracker.Priorities
{
    using System;
    using System.Threading.Tasks;
    using Results;

    public interface IPriorityQueryService
    {
        Task<IPriorityResult[]> QueryAllAsync();

        Task<IPriorityResult> QueryByIdAsync(Guid id);
    }
}
