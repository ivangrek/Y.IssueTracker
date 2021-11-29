namespace Y.IssueTracker.Web.Models.Project;

using Projects.Commands;

public sealed class CreateProjectViewModel : ICreateCommand
{
    public string Name { get; set; }
}
