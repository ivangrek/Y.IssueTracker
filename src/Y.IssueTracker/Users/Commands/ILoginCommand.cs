namespace Y.IssueTracker.Users.Commands
{
    public interface ILoginCommand
    {
        string Email { get; }

        string Password { get; }

        bool RememberMe { get; }
    }
}
