namespace Y.IssueTracker.Issues;

using System;
using System.Threading.Tasks;
using Results;

public interface IIssueQueryService
{
    Task<IIssueForListItemResult[]> QueryIssuesForListAsync();

    Task<IIssueResult> QueryByIdAsync(Guid id);

    Task<IIssueForViewResult> QueryIssueForViewAsync(Guid id);
}
