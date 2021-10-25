namespace Y.IssueTracker.Categories.Results
{
    using System;

    public interface ICategoryResult
    {
        Guid Id { get; }

        string Name { get; }

        bool IsActive { get; }
    }
}
