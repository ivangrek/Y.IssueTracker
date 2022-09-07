namespace Y.IssueTracker.Categories.Commands;

public sealed class UpdateCommand
{
    public Guid Id { get; set; }

    public string Name { get; set; }
}
