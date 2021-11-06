namespace Y.IssueTracker.Web.Infrastructure.QueryServices.Results
{
    using System;
    using Users.Domain;
    using Users.Results;

    internal sealed class UserResult : IUserResult
    {
        public Guid Id { get; init; }

        public string Name { get; init; }

        public Role Role { get; init; }

        public bool IsActive { get; init; }

        public bool IsDefault { get; init; }
    }
}
