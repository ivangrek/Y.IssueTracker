namespace Y.IssueTracker.Web.Models.Account;

using System.ComponentModel.DataAnnotations;

public sealed class RegisterViewModel
{
    public string Email { get; init; }

    public string Password { get; init; }

    [Display(Name = "Confirm password")]
    public string PasswordConfirm { get; init; }
}
