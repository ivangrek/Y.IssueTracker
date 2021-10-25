namespace Y.IssueTracker.Web.Models.Account
{
    using System.ComponentModel.DataAnnotations;

    public sealed class LoginViewModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
