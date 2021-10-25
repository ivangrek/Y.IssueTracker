namespace Y.IssueTracker.Categories.Commands
{
    using System;

    public interface IActivateCommand
    {
        Guid Id { get; }
    }
}
