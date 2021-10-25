namespace Y.IssueTracker.Comments.Commands
{
    using System;

    public interface ICreateCommand
    {
        Guid IssueId { get; }

        string Text { get; }

        Guid AuthorUserId { get; }
    }
}
