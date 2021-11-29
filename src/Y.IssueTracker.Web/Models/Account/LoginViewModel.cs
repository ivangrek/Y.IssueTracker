namespace Y.IssueTracker.Web.Models.Account
{
    using System.ComponentModel.DataAnnotations;
    using Y.IssueTracker.Users.Commands;

    public sealed class LoginViewModel : ILoginCommand
    {
        public string Email { get; set; }

        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
