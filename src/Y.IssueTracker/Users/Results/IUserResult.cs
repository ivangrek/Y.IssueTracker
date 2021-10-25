namespace Y.IssueTracker.Users.Results
{
    using System;
    using Domain;

    public interface IUserResult
    {
        Guid Id { get; }

        string Name { get; }

        Role Role { get; }

        bool IsActive { get; }
    }
}
