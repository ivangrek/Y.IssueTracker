namespace Y.IssueTracker.Web.Infrastructure
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Users;

    internal sealed class AccountService : IAccountService
    {
        public Task SignOutAsync(Guid userId)
        {
            var ticketKeys = SimpleTicketStore.Tickets
                .Where(x => x.Value.UserId == userId)
                .Select(x => x.Key);

            foreach (var ticketKey in ticketKeys)
            {
                SimpleTicketStore.Tickets.Remove(ticketKey);
            }

            return Task.CompletedTask;
        }
    }
}
