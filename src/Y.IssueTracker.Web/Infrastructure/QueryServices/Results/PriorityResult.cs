namespace Y.IssueTracker.Web.Infrastructure.QueryServices.Results;

using System;
using Priorities.Results;

internal sealed class PriorityResult : IPriorityResult
{
    public Guid Id { get; init; }

    public string Name { get; init; }

    public int Weight { get; init; }

    public bool IsActive { get; init; }
}
