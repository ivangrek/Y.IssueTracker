namespace Y.IssueTracker.Users.Commands;

using System;

public interface IDeactivateCommand
{
    Guid Id { get; }
}
