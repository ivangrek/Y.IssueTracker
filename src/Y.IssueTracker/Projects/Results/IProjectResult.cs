namespace Y.IssueTracker.Projects.Results
{
    using System;

    public interface IProjectResult
    {
        Guid Id { get; }

        string Name { get; }

        bool IsActive { get; }
    }
}
