namespace Y.IssueTracker.Users
{
    using System.Threading.Tasks;

    public interface IEmailService
    {
        Task SendNewPasswordAsync(string email, string password);
    }
}
