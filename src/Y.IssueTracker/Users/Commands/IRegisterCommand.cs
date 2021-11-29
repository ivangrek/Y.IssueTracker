namespace Y.IssueTracker.Users.Commands;

public interface IRegisterCommand
{
    string Email { get; }

    string Password { get; }

    string PasswordConfirm { get; }
}
