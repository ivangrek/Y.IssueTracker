namespace Y.IssueTracker.Web.Infrastructure
{
    using System;
    using Microsoft.AspNetCore.Authentication;

    internal sealed class AccountTicket
    {
        public Guid Id { get; init; }

        public Guid UserId { get; init; }

        public AuthenticationTicket Value { get; init; }
    }
}
