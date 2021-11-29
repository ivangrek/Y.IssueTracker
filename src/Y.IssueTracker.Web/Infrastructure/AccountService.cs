namespace Y.IssueTracker.Web.Infrastructure;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Users;
using Y.IssueTracker.Users.Domain;

internal sealed class AccountService : IAccountService
{
    private readonly IHttpContextAccessor httpContextAccessor;

    public AccountService(IHttpContextAccessor httpContextAccessor)
    {
        this.httpContextAccessor = httpContextAccessor;
    }

    public Task SignInAsync(Guid userId, string name, Role role, bool rememberMe)
    {
        var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Role, role.ToString())
            };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        var properties = new AuthenticationProperties
        {
            IsPersistent = rememberMe
        };

        return this.httpContextAccessor.HttpContext
            .SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, properties);
    }

    public Task SignOutAsync()
    {
        return this.httpContextAccessor.HttpContext
                .SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }

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
