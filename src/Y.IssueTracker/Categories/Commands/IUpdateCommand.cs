namespace Y.IssueTracker.Categories.Commands
{
    using System;

    public interface IUpdateCommand
    {
        Guid Id { get; }

        string Name { get; }
    }
}
