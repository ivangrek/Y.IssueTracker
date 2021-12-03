namespace Y.IssueTracker.Web.Models.Project;

using Projects.Commands;

public sealed class DeactivateProjectViewModel : IDeactivateCommand
{
    public Guid Id { get; set; }

    public string Name { get; set; }
}
