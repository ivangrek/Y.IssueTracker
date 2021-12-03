namespace Y.IssueTracker.Issues.Domain;

public sealed class Issue : IEntity
{
    public Issue(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }

    public string Name { get; set; }

    public string Description { get; set; }

    public Guid ProjectId { get; set; }

    public Guid CategoryId { get; set; }

    public Guid PriorityId { get; set; }

    public IssueStatus Status { get; set; }

    public Guid AssignedUserId { get; set; }

    public Guid AuthorUserId { get; set; }

    public DateTime CreatedOn { get; set; }
}
