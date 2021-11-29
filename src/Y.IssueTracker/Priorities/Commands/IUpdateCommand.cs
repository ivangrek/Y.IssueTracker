namespace Y.IssueTracker.Priorities.Commands;

using System;

public interface IUpdateCommand
{
    Guid Id { get; }

    string Name { get; }

    int Weight { get; }
}
