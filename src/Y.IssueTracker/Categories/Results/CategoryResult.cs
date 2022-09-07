namespace Y.IssueTracker.Categories.Results;

using System;

public sealed class CategoryResult
{
    public Guid Id { get; init; }

    public string Name { get; init; }

    public bool IsActive { get; init; }
}
