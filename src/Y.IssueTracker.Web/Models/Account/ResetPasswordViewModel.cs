namespace Y.IssueTracker.Web.Models.Account;

using Y.IssueTracker.Users.Commands;

public sealed class ResetPasswordViewModel : IResetPasswordCommand
{
    public string Email { get; set; }
}
