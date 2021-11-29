namespace Y.IssueTracker.Web.Models.Priority;

using System;
using Priorities.Commands;

public sealed class UpdatePriorityViewModel : IUpdateCommand
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public int Weight { get; set; }
}
