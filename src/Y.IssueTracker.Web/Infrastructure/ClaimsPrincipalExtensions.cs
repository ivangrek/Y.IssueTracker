namespace Y.IssueTracker.Web.Infrastructure
{
    using System;
    using System.Security;
    using System.Security.Claims;
    using Users.Domain;

    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal principal)
        {
            var claim = principal
                .FindFirst(ClaimTypes.NameIdentifier);

            if (claim is null)
            {
                throw new SecurityException();
            }

            return new Guid(claim.Value);
        }

        public static Role GetUserRole(this ClaimsPrincipal principal)
        {
            var claim = principal
                .FindFirst(ClaimTypes.Role);

            if (claim is null)
            {
                throw new SecurityException();
            }

            return Enum.Parse<Role>(claim.Value);
        }
    }
}