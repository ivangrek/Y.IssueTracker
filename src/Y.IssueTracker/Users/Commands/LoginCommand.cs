namespace Y.IssueTracker.Users.Commands;

public sealed class LoginCommand
{
    public string Email { get; init; }

    public string Password { get; init; }

    public bool RememberMe { get; init; }
}
