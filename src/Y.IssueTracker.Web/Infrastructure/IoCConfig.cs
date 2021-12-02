namespace Y.IssueTracker.Web.Infrastructure;

using System;
using System.IO;
using Categories;
using Categories.Domain;
using Comments;
using Comments.Domain;
using Issues;
using Issues.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Priorities;
using Priorities.Domain;
using Projects;
using Projects.Domain;
using QueryServices;
using Repositories;
using Users;
using Users.Domain;

public static class IoCConfig
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var dbPath = Path.Combine(path, "database.db");

        // for now
        if (File.Exists(dbPath))
        {
            File.Delete(dbPath);
        }

        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite($"Data Source={dbPath}"));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IPriorityRepository, PriorityRepository>();
        services.AddScoped<IIssueRepository, IssueRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IProjectQueryService, ProjectQueryService>();
        services.AddScoped<ICategoryQueryService, CategoryQueryService>();
        services.AddScoped<IPriorityQueryService, PriorityQueryService>();
        services.AddScoped<IIssueQueryService, IssueQueryService>();
        services.AddScoped<ICommentQueryService, CommentQueryService>();
        services.AddScoped<IUserQueryService, UserQueryService>();

        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IEmailService, EmailService>();
    }
}
