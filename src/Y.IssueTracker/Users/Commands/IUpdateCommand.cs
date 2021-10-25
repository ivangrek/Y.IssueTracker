namespace Y.IssueTracker.Users.Commands
{
    using System;
    using Domain;

    public interface IUpdateCommand
    {
        Guid Id { get; }

        string Name { get; }

        Role Role { get; }
    }
}
