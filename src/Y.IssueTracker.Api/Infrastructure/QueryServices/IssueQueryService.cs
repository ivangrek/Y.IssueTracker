namespace Y.IssueTracker.Api.Infrastructure.QueryServices;

using Issues;
using Issues.Results;
using Microsoft.EntityFrameworkCore;
using Y.IssueTracker.Issues.Queries;

internal sealed class IssueQueryService : IIssueQueryService
{
    private readonly ApplicationDbContext applicationDbContext;

    public IssueQueryService(ApplicationDbContext applicationDbContext)
    {
        this.applicationDbContext = applicationDbContext;
    }

    public Task<IssueForListItemResult[]> HandleAsync(GetIssuesForListQuery query)
    {
        var projectQuery = this.applicationDbContext
            .Projects
            .AsNoTracking();

        var categoryQuery = this.applicationDbContext
            .Categories
            .AsNoTracking();

        var priorityQuery = this.applicationDbContext
            .Priorities
            .AsNoTracking();

        var userQuery = this.applicationDbContext
            .Users
            .AsNoTracking();

        var issueQuery = this.applicationDbContext
            .Issues
            .AsNoTracking();

        var resultQuery = issueQuery
            .Join(projectQuery, x => x.ProjectId, x => x.Id, (x, y) => new { issue = x, project = y })
            .Join(categoryQuery, x => x.issue.CategoryId, x => x.Id, (x, y) => new { x.issue, x.project, category = y })
            .Join(priorityQuery, x => x.issue.PriorityId, x => x.Id, (x, y) => new { x.issue, x.project, x.category, priority = y });
            // TODO
            //.Join(userQuery, x => x.issue.AuthorUserId, x => x.Id, (x, y) => new { x.issue, x.project, x.category, x.priority, author = y });

        return resultQuery
            .GroupJoin(userQuery, x => x.issue.AssignedUserId, x => x.Id, (x, y) => new { result = x, assigned = y })
            .SelectMany(x => x.assigned.DefaultIfEmpty(), (x, y) => new { x.result, assigned = y })
            .Select(x => new IssueForListItemResult
            {
                Id = x.result.issue.Id,
                Name = x.result.issue.Name,
                Project = x.result.project.Name,
                Category = x.result.category.Name,
                Priority = x.result.priority.Name,
                Status = x.result.issue.Status,
                AssignedUserName = x.assigned == null ? string.Empty : x.assigned.Name,
                // TODO
                AuthorUserName = "--Not implement--", //x.result.author.Name,
                CreatedOn = x.result.issue.CreatedOn
            })
            .ToArrayAsync();
    }

    public Task<IssueResult> HandleAsync(GetByIdQuery query)
    {
        return this.applicationDbContext
            .Issues
            .AsNoTracking()
            .Where(x => x.Id == query.Id)
            .Select(x => new IssueResult
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                ProjectId = x.ProjectId,
                CategoryId = x.CategoryId,
                PriorityId = x.PriorityId,
                Status = x.Status,
                AssignedUserId = x.AssignedUserId,
                AuthorUserId = x.AuthorUserId,
                CreatedOn = x.CreatedOn
            })
            .SingleOrDefaultAsync();
    }

    public Task<IssueForViewResult> HandleAsync(GetIssueForViewQuery query)
    {
        var projectQuery = this.applicationDbContext
            .Projects
            .AsNoTracking();

        var categoryQuery = this.applicationDbContext
            .Categories
            .AsNoTracking();

        var priorityQuery = this.applicationDbContext
            .Priorities
            .AsNoTracking();

        var userQuery = this.applicationDbContext
            .Users
            .AsNoTracking();

        var issueQuery = this.applicationDbContext
            .Issues
            .AsNoTracking()
            .Where(x => x.Id == query.Id);

        var resultQuery = issueQuery
            .Join(projectQuery, x => x.ProjectId, x => x.Id, (x, y) => new { issue = x, project = y })
            .Join(categoryQuery, x => x.issue.CategoryId, x => x.Id, (x, y) => new { x.issue, x.project, category = y })
            .Join(priorityQuery, x => x.issue.PriorityId, x => x.Id, (x, y) => new { x.issue, x.project, x.category, priority = y });
            // TODO
            //.Join(userQuery, x => x.issue.AuthorUserId, x => x.Id, (x, y) => new { x.issue, x.project, x.category, x.priority, author = y });

        return resultQuery
            .GroupJoin(userQuery, x => x.issue.AssignedUserId, x => x.Id, (x, y) => new { result = x, assigned = y })
            .SelectMany(x => x.assigned.DefaultIfEmpty(), (x, y) => new { x.result, assigned = y })
            .Select(x => new IssueForViewResult
            {
                Id = x.result.issue.Id,
                Name = x.result.issue.Name,
                Description = x.result.issue.Description,
                ProjectName = x.result.project.Name,
                CategoryName = x.result.category.Name,
                PriorityName = x.result.priority.Name,
                Status = x.result.issue.Status,
                AssignedUserId = x.assigned == null ? Guid.Empty : x.assigned.Id,
                AssignedUserName = x.assigned == null ? string.Empty : x.assigned.Name,
                // TODO
                AuthorUserId = Guid.Empty, //x.result.author.Id,
                AuthorUserName = "--Not implement--", //x.result.author.Name,
                CreatedOn = x.result.issue.CreatedOn
            })
            .SingleOrDefaultAsync();
    }
}
