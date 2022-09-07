namespace Y.IssueTracker.Web.Models.Issue;

using System.ComponentModel;

public sealed class CreateIssueViewModel
{
    public string Name { get; init; }

    public string Description { get; init; }

    [DisplayName("Project")]
    public Guid ProjectId { get; init; }

    [DisplayName("Category")]
    public Guid CategoryId { get; init; }

    [DisplayName("Priority")]
    public Guid PriorityId { get; init; }

    [DisplayName("Assigned")]
    public Guid AssignedUserId { get; init; }

    public Guid AuthorUserId { get; init; }
}
