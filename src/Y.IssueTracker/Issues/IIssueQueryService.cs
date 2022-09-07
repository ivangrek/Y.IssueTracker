namespace Y.IssueTracker.Issues;

using Results;
using Y.IssueTracker.Issues.Queries;

public interface IIssueQueryService
{
    Task<IssueForListItemResult[]> HandleAsync(GetIssuesForListQuery query);

    Task<IssueResult> HandleAsync(GetByIdQuery query);

    Task<IssueForViewResult> HandleAsync(GetIssueForViewQuery query);
}
