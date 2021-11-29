namespace Y.IssueTracker.Projects.Commands;

using System;

public interface IUpdateCommand
{
    Guid Id { get; }

    string Name { get; }
}
