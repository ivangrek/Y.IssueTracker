﻿namespace Y.IssueTracker.Web.Models.Priority;

using Priorities.Commands;

public sealed class DeletePriorityViewModel : IDeleteCommand
{
    public Guid Id { get; set; }

    public string Name { get; set; }
}
