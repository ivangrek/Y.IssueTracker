namespace Y.IssueTracker.Web.Models.Issue;

using System;
using Comments.Results;
using Issues.Results;

public sealed class ViewIssueViewModel
{
    public ViewIssueViewModel()
    {
        Comments = Array.Empty<ICommentForViewResult>();
    }

    public IIssueForViewResult Issue { get; set; }

    public ICommentForViewResult[] Comments { get; set; }
}
