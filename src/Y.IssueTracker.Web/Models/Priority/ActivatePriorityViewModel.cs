﻿namespace Y.IssueTracker.Web.Models.Priority;

using Priorities.Commands;

public sealed class ActivatePriorityViewModel : IActivateCommand
{
    public Guid Id { get; set; }

    public string Name { get; set; }
}
