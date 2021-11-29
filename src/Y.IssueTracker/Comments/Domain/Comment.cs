namespace Y.IssueTracker.Comments.Domain;

using System;

public sealed class Comment : IEntity
{
    public Comment(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }

    public Guid IssueId { get; set; }

    public string Text { get; set; }

    public Guid AuthorUserId { get; set; }

    public DateTime CreatedOn { get; set; }
}
