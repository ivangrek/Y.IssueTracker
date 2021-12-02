namespace Y.IssueTracker.Web.Infrastructure;

using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using System.Threading.Tasks;
using Y.IssueTracker.Users;
using Y.IssueTracker.Web.Infrastructure.Options;

internal sealed class EmailService : IEmailService
{
    private readonly IOptions<EmailServiceOptions> emailOptions;

    public EmailService(IOptions<EmailServiceOptions> emailOptions)
    {
        this.emailOptions = emailOptions;
    }

    public async Task SendNewPasswordAsync(string email, string password)
    {
        var options = this.emailOptions.Value;
        var emailMessage = new MimeMessage();

        emailMessage.From.Add(new MailboxAddress("Site administration", options.UserName));
        emailMessage.To.Add(new MailboxAddress(string.Empty, email));
        emailMessage.Subject = "New password.";

        emailMessage.Body = new TextPart(TextFormat.Html)
        {
            Text = $"Please use this new password: {password}"
        };

        using var client = new SmtpClient();

        await client.ConnectAsync(options.SmtpServer, options.SmtpPort, SecureSocketOptions.StartTls);
        await client.AuthenticateAsync(options.UserName, options.UserPassword);
        await client.SendAsync(emailMessage);
        await client.DisconnectAsync(true);
    }
}
