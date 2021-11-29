namespace Y.IssueTracker.Categories.Domain;

using System;

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
