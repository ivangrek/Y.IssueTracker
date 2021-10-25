namespace Y.IssueTracker.Web.Infrastructure
{
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
            services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase(databaseName: "IssueTracker"));

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
        }
    }
}
