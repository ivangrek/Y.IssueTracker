namespace Y.IssueTracker.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Categories;
    using Comments;
    using Infrastructure;
    using Issues;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.Issue;
    using Priorities;
    using Projects;
    using Users;

    [Authorize]
    public sealed class IssueController : Controller
    {
        private readonly IIssueCommandService issueCommandService;
        private readonly IIssueQueryService issueQueryService;
        private readonly IProjectQueryService projectQueryService;
        private readonly ICategoryQueryService categoryQueryService;
        private readonly IPriorityQueryService priorityQueryService;
        private readonly ICommentQueryService commentQueryService;
        private readonly IUserQueryService userQueryService;

        public IssueController(
            IIssueCommandService issueCommandService,
            IIssueQueryService issueQueryService,
            IProjectQueryService projectQueryService,
            ICategoryQueryService categoryQueryService,
            IPriorityQueryService priorityQueryService,
            ICommentQueryService commentQueryService,
            IUserQueryService userQueryService)
        {
            this.issueCommandService = issueCommandService;
            this.issueQueryService = issueQueryService;
            this.projectQueryService = projectQueryService;
            this.categoryQueryService = categoryQueryService;
            this.priorityQueryService = priorityQueryService;
            this.commentQueryService = commentQueryService;
            this.userQueryService = userQueryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var issues = await this.issueQueryService
                .QueryIssuesForListAsync();

            return View(issues);
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var issueTask = this.issueQueryService
                .QueryIssueForViewAsync(id);

            var commentsTask = this.commentQueryService
                .QueryCommentsForViewAsync(id);

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
        public async Task<IActionResult> Create()
        {
            await InitDropdownListsAsync();

            var viewModel = new CreateIssueViewModel();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateIssueViewModel viewModel)
        {
            viewModel.AuthorUserId = User.GetUserId();

            var result = await this.issueCommandService
                .ExecuteAsync(viewModel);

            if (result.Status is ResultStatus.Success)
            {
                return RedirectToAction(nameof(Index));
            }

            if (result.Status is ResultStatus.Invalid)
            {
                foreach (var (key, value) in result.Errors)
                {
                    ModelState.AddModelError(key, value);
                }

                await InitDropdownListsAsync();

                return View(viewModel);
            }

            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var issue = await this.issueQueryService
                .QueryByIdAsync(id);

            if (issue is null)
            {
                return BadRequest();
            }

            await InitDropdownListsAsync();

            var viewModel = new UpdateIssueViewModel
            {
                Id = issue.Id,
                Name = issue.Name,
                Description = issue.Description,
                ProjectId = issue.ProjectId,
                CategoryId = issue.CategoryId,
                PriorityId = issue.PriorityId,
                Status = issue.Status,
                AssignedUserId = issue.AssignedUserId
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateIssueViewModel viewModel)
        {
            var result = await this.issueCommandService
                .ExecuteAsync(viewModel);

            if (result.Status is ResultStatus.Success)
            {
                return RedirectToAction(nameof(View), new { id = viewModel.Id });
            }

            if (result.Status is ResultStatus.Invalid)
            {
                foreach (var (key, value) in result.Errors)
                {
                    ModelState.AddModelError(key, value);
                }

                await InitDropdownListsAsync();

                return View(viewModel);
            }

            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var issue = await this.issueQueryService
                .QueryByIdAsync(id);

            if (issue is null)
            {
                return BadRequest();
            }

            var viewModel = new DeleteIssueViewModel
            {
                Name = issue.Name
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DeleteIssueViewModel viewModel)
        {
            var result = await this.issueCommandService
                .ExecuteAsync(viewModel);

            if (result.Status is ResultStatus.Success)
            {
                return RedirectToAction(nameof(Index));
            }

            if (result.Status is ResultStatus.Invalid)
            {
                foreach (var (key, value) in result.Errors)
                {
                    ModelState.AddModelError(key, value);
                }

                return View(viewModel);
            }

            return BadRequest();
        }

        private async Task InitDropdownListsAsync()
        {
            var projectsTask = this.projectQueryService
                .QueryAllAsync();

            var categoriesTask = this.categoryQueryService
                .QueryAllAsync();

            var prioritiesTask = this.priorityQueryService
                .QueryAllAsync();

            var usersTask = this.userQueryService
                .QueryAllAsync();

            var projects = await projectsTask;
            var categories = await categoriesTask;
            var priorities = await prioritiesTask;
            var users = await usersTask;

            ViewBag.Projects = projects
                .Where(x => x.IsActive)
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()));

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
}
