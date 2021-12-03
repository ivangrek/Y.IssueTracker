namespace Y.IssueTracker.Priorities;

using Results;

public interface IPriorityQueryService
{
    Task<IPriorityResult[]> QueryAllAsync();

    Task<IPriorityResult> QueryByIdAsync(Guid id);
}
