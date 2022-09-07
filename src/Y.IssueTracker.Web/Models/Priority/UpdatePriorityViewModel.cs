namespace Y.IssueTracker.Web.Models.Priority;

public sealed class UpdatePriorityViewModel
{
    public Guid Id { get; init; }

    public string Name { get; init; }

    public int Weight { get; init; }
}
