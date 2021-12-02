namespace Y.IssueTracker.Users.Commands;

public interface IResetPasswordCommand
{
    string Email { get; }
}
