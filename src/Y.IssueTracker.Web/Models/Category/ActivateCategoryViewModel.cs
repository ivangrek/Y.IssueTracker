namespace Y.IssueTracker.Web.Models.Category;

using Categories.Commands;

public sealed class ActivateCategoryViewModel : IActivateCommand
{
    public Guid Id { get; set; }

    public string Name { get; set; }
}
