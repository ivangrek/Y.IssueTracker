namespace Y.IssueTracker.Users.Commands
{
    using System;

    public interface IActivateCommand
    {
        Guid Id { get; }
    }
}
