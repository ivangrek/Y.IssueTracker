namespace Y.IssueTracker.Web.Models.Comment;

using System;
using Comments.Commands;

public sealed class CreateCommentViewModel : ICreateCommand
{
    public Guid IssueId { get; set; }

    public string Text { get; set; }

    public Guid AuthorUserId { get; set; }
}
