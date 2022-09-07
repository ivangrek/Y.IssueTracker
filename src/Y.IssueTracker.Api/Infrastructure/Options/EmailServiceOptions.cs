namespace Y.IssueTracker.Api.Infrastructure.Options
{
    internal sealed class EmailServiceOptions
    {
        public string SmtpServer { get; init; }

        public int SmtpPort { get; init; }

        public string UserName { get; init; }

        public string UserPassword { get; init; }
    }
}
