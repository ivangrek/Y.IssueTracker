namespace Y.IssueTracker.Priorities.Commands;

using System;

public interface IDeactivateCommand
{
    Guid Id { get; }
}
