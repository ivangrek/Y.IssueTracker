namespace Y.IssueTracker.Web.Models.Category;

using System;
using Categories.Commands;

public sealed class DeactivateCategoryViewModel : IDeactivateCommand
{
    public Guid Id { get; set; }

    public string Name { get; set; }
}
