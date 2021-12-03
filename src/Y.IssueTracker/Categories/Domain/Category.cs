namespace Y.IssueTracker.Categories.Domain;

public sealed class Category : IEntity
{
    public Category(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }

    public string Name { get; set; }

    public bool IsActive { get; set; }
}
