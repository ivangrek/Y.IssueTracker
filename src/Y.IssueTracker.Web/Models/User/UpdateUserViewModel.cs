namespace Y.IssueTracker.Web.Models.User;

using Users.Domain;

public sealed class UpdateUserViewModel
{
    public Guid Id { get; init; }

    public string Name { get; init; }

    public Role Role { get; init; }
}
