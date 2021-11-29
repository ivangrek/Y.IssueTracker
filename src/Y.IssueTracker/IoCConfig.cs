namespace Y.IssueTracker;

using Categories;
using Comments;
using Issues;
using Microsoft.Extensions.DependencyInjection;
using Priorities;
using Projects;
using Users;

public static class IoCConfig
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IProjectCommandService, ProjectCommandService>();
        services.AddScoped<ICategoryCommandService, CategoryCommandService>();
        services.AddScoped<IPriorityCommandService, PriorityCommandService>();
        services.AddScoped<IIssueCommandService, IssueCommandService>();
        services.AddScoped<ICommentCommandService, CommentCommandService>();
        services.AddScoped<IUserCommandService, UserCommandService>();
    }
}
