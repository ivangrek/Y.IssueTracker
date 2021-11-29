namespace Y.IssueTracker.Web.Models.Account;

using System.ComponentModel.DataAnnotations;
using Y.IssueTracker.Users.Commands;

public sealed class RegisterViewModel : IRegisterCommand
{
    public string Email { get; set; }

    public string Password { get; set; }

    [Display(Name = "Confirm password")]
    public string PasswordConfirm { get; set; }
}
