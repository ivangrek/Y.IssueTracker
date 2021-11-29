namespace Y.IssueTracker.Categories.Commands;

using System;

public interface IDeactivateCommand
{
    Guid Id { get; }
}
