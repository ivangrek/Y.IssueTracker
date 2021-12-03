namespace Y.IssueTracker.Web.Models.Issue;

using System.ComponentModel;
using Issues.Commands;

public sealed class CreateIssueViewModel : ICreateCommand
{
    public string Name { get; set; }

    public string Description { get; set; }

    [DisplayName("Project")]
    public Guid ProjectId { get; set; }

    [DisplayName("Category")]
    public Guid CategoryId { get; set; }

    [DisplayName("Priority")]
    public Guid PriorityId { get; set; }

    [DisplayName("Assigned")]
    public Guid AssignedUserId { get; set; }

    public Guid AuthorUserId { get; set; }
}
