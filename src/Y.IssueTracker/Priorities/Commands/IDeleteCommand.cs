namespace Y.IssueTracker.Priorities.Commands;

using System;

public interface IDeleteCommand
{
    Guid Id { get; }
}
