namespace Y.IssueTracker.Priorities.Domain;

public sealed class Priority : IEntity
{
    public Priority(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }

    public string Name { get; set; }

    public int Weight { get; set; }

    public bool IsActive { get; set; }
}
