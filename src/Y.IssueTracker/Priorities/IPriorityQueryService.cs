namespace Y.IssueTracker.Priorities;

using Results;
using Y.IssueTracker.Priorities.Queries;

public interface IPriorityQueryService
{
    Task<PriorityResult[]> HandleAsync(GetAllQuery query);

    Task<PriorityResult> HandleAsync(GetByIdQuery query);
}
