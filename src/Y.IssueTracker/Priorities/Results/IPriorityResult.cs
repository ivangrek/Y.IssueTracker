namespace Y.IssueTracker.Priorities.Results;

using System;

public interface IPriorityResult
{
    Guid Id { get; }

    string Name { get; }

    int Weight { get; }

    bool IsActive { get; }
}
