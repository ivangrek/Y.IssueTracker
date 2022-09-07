namespace Y.IssueTracker.Web.Models.Issue;

using Comments.Results;
using Issues.Results;

public sealed class ViewIssueViewModel
{
    public ViewIssueViewModel()
    {
        Comments = Array.Empty<CommentForViewResult>();
    }

    public IssueForViewResult Issue { get; init; }

    public CommentForViewResult[] Comments { get; init; }
}
