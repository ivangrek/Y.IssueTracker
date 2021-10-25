namespace Y.IssueTracker.Users.Commands
{
    using System;

    public interface IDeleteCommand
    {
        Guid Id { get; }
    }
}
