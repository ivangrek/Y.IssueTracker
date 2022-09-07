namespace Y.IssueTracker.Web.Infrastructure;

using Microsoft.Extensions.DependencyInjection;
using Users;

public static class IoCConfig
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();
    }
}
