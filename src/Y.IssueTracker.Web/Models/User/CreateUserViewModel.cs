namespace Y.IssueTracker.Web.Models.User;

using Users.Domain;

public sealed class CreateUserViewModel
{
    public string Name { get; init; }

    public Role Role { get; init; }
}
