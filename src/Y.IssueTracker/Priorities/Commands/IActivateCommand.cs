namespace Y.IssueTracker.Priorities.Commands;

using System;

public interface IActivateCommand
{
    Guid Id { get; }
}
