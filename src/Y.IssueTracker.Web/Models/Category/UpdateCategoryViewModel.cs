namespace Y.IssueTracker.Web.Models.Category;

using System;
using Categories.Commands;

public sealed class UpdateCategoryViewModel : IUpdateCommand
{
    public Guid Id { get; set; }

    public string Name { get; set; }
}
