namespace Y.IssueTracker.Web.Controllers;

using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.Issue;
using Y.IssueTracker.Comments.Queries;
using Y.IssueTracker.Issues.Commands;
using Y.IssueTracker.Issues.Queries;
using Y.IssueTracker.Web.Services;

[Authorize]
public sealed class IssueController : Controller
{
    private readonly IIssueService issueService;
    private readonly IProjectService projectService;
    private readonly ICategoryService categoryService;
    private readonly IPriorityService priorityService;
    private readonly ICommentService commentService;
    private readonly IUserService userService;

    public IssueController(
        IIssueService issueService,
        IProjectService projectService,
        ICategoryService categoryService,
        IPriorityService priorityService,
        ICommentService commentService,
        IUserService userService)
    {
        this.issueService = issueService;
        this.projectService = projectService;
        this.categoryService = categoryService;
        this.priorityService = priorityService;
        this.commentService = commentService;
        this.userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> IndexAsync(int? page, int? pageCount)
    {
        if (page < 1 || pageCount < 1)
        {
            return NotFound();
        }

        var query = new GetIssuesForListQuery
        {
            Page = page ?? 1,
            PageCount = pageCount ?? int.MaxValue
        };

        var result = await this.issueService
            .HandleAsync(query);

        return View(result);
    }

    [HttpGet]
    public async Task<IActionResult> ViewAsync(Guid id)
    {
        var issueTask = this.issueService
            .HandleAsync(new Issues.Queries.GetIssueForViewQuery { Id = id });

        var commentsTask = this.commentService
            .HandleAsync(new GetCommentsForViewQuery { IssueId = id });

        var issue = await issueTask;
        var comments = await commentsTask;

        var viewModel = new ViewIssueViewModel
        {
            Issue = issue,
            Comments = comments
        };

        return View(viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> CreateAsync()
    {
        await InitDropdownListsAsync();

        var viewModel = new CreateIssueViewModel();

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateAsync(CreateIssueViewModel viewModel)
    {
        var command = new CreateCommand
        {
            Name = viewModel.Name,
            Description = viewModel.Description,
            ProjectId = viewModel.ProjectId,
            CategoryId = viewModel.CategoryId,
            PriorityId = viewModel.PriorityId,
            AssignedUserId = viewModel.AssignedUserId,
            AuthorUserId = User.GetUserId()
        };

        var result = await this.issueService
            .HandleAsync(command);

        if (result.Status is ResultStatus.Success)
        {
            return RedirectToAction(nameof(Index));
        }

        if (result.Status is ResultStatus.Invalid)
        {
            ModelState.AddModelErrors(result.Errors);

            await InitDropdownListsAsync();

            return View(viewModel);
        }

        return StatusCode(StatusCodes.Status500InternalServerError);
    }

    [HttpGet]
    public async Task<IActionResult> UpdateAsync(Guid id)
    {
        var query = new Issues.Queries.GetByIdQuery
        {
            Id = id
        };

        var result = await this.issueService
            .HandleAsync(query);

        if (result is null)
        {
            return NotFound();
        }

        await InitDropdownListsAsync();

        var viewModel = new UpdateIssueViewModel
        {
            Id = result.Id,
            Name = result.Name,
            Description = result.Description,
            ProjectId = result.ProjectId,
            CategoryId = result.CategoryId,
            PriorityId = result.PriorityId,
            Status = result.Status,
            AssignedUserId = result.AssignedUserId
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateAsync(UpdateIssueViewModel viewModel)
    {
        var command = new UpdateCommand
        {
            Id = viewModel.Id,
            Name = viewModel.Name,
            Description = viewModel.Description,
            ProjectId = viewModel.ProjectId,
            CategoryId = viewModel.CategoryId,
            PriorityId = viewModel.PriorityId,
            Status = viewModel.Status,
            AssignedUserId = viewModel.AssignedUserId
        };

        var result = await this.issueService
            .HandleAsync(command);

        if (result.Status is ResultStatus.Success)
        {
            return RedirectToAction(nameof(View), new { id = viewModel.Id });
        }

        if (result.Status is ResultStatus.Invalid)
        {
            ModelState.AddModelErrors(result.Errors);

            await InitDropdownListsAsync();

            return View(viewModel);
        }

        return StatusCode(StatusCodes.Status500InternalServerError);
    }

    [HttpGet]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var query = new Issues.Queries.GetByIdQuery
        {
            Id = id
        };

        var result = await this.issueService
            .HandleAsync(query);

        if (result is null)
        {
            return NotFound();
        }

        var viewModel = new DeleteIssueViewModel
        {
            Name = result.Name
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteAsync(DeleteIssueViewModel viewModel)
    {
        var command = new DeleteCommand
        {
            Id = viewModel.Id
        };

        var result = await this.issueService
            .HandleAsync(command);

        if (result.Status is ResultStatus.Success)
        {
            return RedirectToAction(nameof(Index));
        }

        if (result.Status is ResultStatus.Invalid)
        {
            ModelState.AddModelErrors(result.Errors);

            return View(viewModel);
        }

        return StatusCode(StatusCodes.Status500InternalServerError);
    }

    private async Task InitDropdownListsAsync()
    {
        var projectsTask = this.projectService
            .HandleAsync(new Projects.Queries.GetAllQuery { Page = 1, PageCount = int.MaxValue });

        var categoriesTask = this.categoryService
            .HandleAsync(new Categories.Queries.GetAllQuery { Page = 1, PageCount = int.MaxValue });

        var prioritiesTask = this.priorityService
            .HandleAsync(new Priorities.Queries.GetAllQuery { Page = 1, PageCount = int.MaxValue });

        var usersTask = this.userService
            .HandleAsync(new Users.Queries.GetAllQuery { Page = 1, PageCount = int.MaxValue });

        var projects = await projectsTask;
        var categories = await categoriesTask;
        var priorities = await prioritiesTask;
        var users = await usersTask;

        ViewBag.Projects = projects
            .Where(x => x.IsActive)
            .Select(x => new SelectListItem(x.Name, x.Id.ToString()));

        // TODO
        ViewBag.Categories = categories
            .Where(x => x.IsActive)
            .Select(x => new SelectListItem(x.Name, x.Id.ToString()));

        ViewBag.Priorities = priorities
            .Where(x => x.IsActive)
            .OrderBy(x => x.Weight)
            .Select(x => new SelectListItem(x.Name, x.Id.ToString()));

        ViewBag.Users = Enumerable.Repeat(new SelectListItem("-- None --", Guid.Empty.ToString()), 1)
            .Union(users
                .Where(x => x.IsActive)
                .Select(x => new SelectListItem(x.Name, x.Id.ToString())));
    }
}
