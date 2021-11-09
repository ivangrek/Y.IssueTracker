namespace Y.IssueTracker.Web.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;

    internal sealed class SimpleTicketStore : ITicketStore
    {
        public static Dictionary<string, AccountTicket> Tickets = new Dictionary<string, AccountTicket>();

        public Task<string> StoreAsync(AuthenticationTicket ticket)
        {
            var key = Guid.NewGuid()
                .ToString();

            StoreTicket(key, ticket);

            return Task.FromResult(key);
        }

        public Task RenewAsync(string key, AuthenticationTicket ticket)
        {
            StoreTicket(key, ticket);

            return Task.CompletedTask;
        }

        public Task<AuthenticationTicket> RetrieveAsync(string key)
        {
            Tickets.TryGetValue(key, out var accountTicket);

            return Task.FromResult(accountTicket?.Value);
        }

        public Task RemoveAsync(string key)
        {
            Tickets.Remove(key);

            return Task.CompletedTask;
        }

        private static void StoreTicket(string key, AuthenticationTicket ticket)
        {
            var accountTicket = new AccountTicket
            {
                Id = Guid.Parse(key),
                UserId = ticket.Principal.GetUserId(),
                Value = ticket
            };

            Tickets[key] = accountTicket;
        }
    }
}
