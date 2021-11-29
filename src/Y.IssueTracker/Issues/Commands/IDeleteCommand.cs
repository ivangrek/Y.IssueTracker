namespace Y.IssueTracker.Issues.Commands;

using System;

public interface IDeleteCommand
{
    Guid Id { get; }
}
