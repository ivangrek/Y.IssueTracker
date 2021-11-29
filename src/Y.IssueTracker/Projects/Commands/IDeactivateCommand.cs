namespace Y.IssueTracker.Projects.Commands;

using System;

public interface IDeactivateCommand
{
    Guid Id { get; }
}
