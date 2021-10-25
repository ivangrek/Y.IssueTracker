namespace Y.IssueTracker.Web.Infrastructure.QueryServices.Results
{
    using System;
    using Projects.Results;

    internal sealed class ProjectResult : IProjectResult
    {
        public Guid Id { get; init; }

        public string Name { get; init; }

        public bool IsActive { get; init; }
    }
}
