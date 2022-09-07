namespace Y.IssueTracker.Users.Commands;

public sealed class RegisterCommand
{
    public string Email { get; init; }

    public string Password { get; init; }

    public string PasswordConfirm { get; init; }
}
