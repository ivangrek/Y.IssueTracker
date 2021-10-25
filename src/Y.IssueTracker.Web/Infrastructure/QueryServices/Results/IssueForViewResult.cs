namespace Y.IssueTracker.Web.Infrastructure.QueryServices.Results
{
    using System;
    using Issues.Domain;
    using Issues.Results;

    internal sealed class IssueForViewResult : IIssueForViewResult
    {
        public Guid Id { get; init; }

        public string Name { get; init; }

        public string Description { get; init; }

        public string ProjectName { get; init; }

        public string CategoryName { get; init; }

        public string PriorityName { get; init; }

        public IssueStatus Status { get; init; }

        public Guid AssignedUserId { get; init; }

        public string AssignedUserName { get; init; }

        public Guid AuthorUserId { get; init; }

        public string AuthorUserName { get; init; }

        public DateTime CreatedOn { get; init; }
    }
}
