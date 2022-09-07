namespace Y.IssueTracker.Web.Models.Issue;

using System.ComponentModel;
using Issues.Domain;

public sealed class UpdateIssueViewModel
{
    public Guid Id { get; init; }

    public string Name { get; init; }

    public string Description { get; init; }

    [DisplayName("Project")]
    public Guid ProjectId { get; init; }

    [DisplayName("Category")]
    public Guid CategoryId { get; init; }

    [DisplayName("Priority")]
    public Guid PriorityId { get; init; }

    [DisplayName("Status")]
    public IssueStatus Status { get; init; }

    [DisplayName("Assigned")]
    public Guid AssignedUserId { get; init; }
}
