namespace Y.IssueTracker.Web.Models.Account;

using System.ComponentModel.DataAnnotations;

public sealed class LoginViewModel
{
    public string Email { get; init; }

    public string Password { get; init; }

    [Display(Name = "Remember me?")]
    public bool RememberMe { get; init; }
}
