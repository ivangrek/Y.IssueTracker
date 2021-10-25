namespace Y.IssueTracker.Categories.Commands
{
    using System;

    public interface IDeleteCommand
    {
        Guid Id { get; }
    }
}
