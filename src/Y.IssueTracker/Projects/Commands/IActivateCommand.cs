namespace Y.IssueTracker.Projects.Commands;

using System;

public interface IActivateCommand
{
    Guid Id { get; }
}
