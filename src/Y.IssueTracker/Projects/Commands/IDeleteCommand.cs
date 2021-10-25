namespace Y.IssueTracker.Projects.Commands
{
    using System;

    public interface IDeleteCommand
    {
        Guid Id { get; }
    }
}
