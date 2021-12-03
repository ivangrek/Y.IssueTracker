namespace Y.IssueTracker.Web.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Project;
using Projects;
using Y.IssueTracker.Web.Infrastructure;

[Authorize(Roles = "Administrator,Manager")]
public sealed class ProjectController : Controller
{
    private readonly IProjectCommandService projectCommandService;
    private readonly IProjectQueryService projectQueryService;

    public ProjectController(
        IProjectCommandService projectCommandService,
        IProjectQueryService projectQueryService)
    {
        this.projectCommandService = projectCommandService;
        this.projectQueryService = projectQueryService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var projects = await this.projectQueryService
            .QueryAllAsync();

        return View(projects);
    }

    [HttpGet]
    public IActionResult Create()
    {
        var viewModel = new CreateProjectViewModel();

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateProjectViewModel viewModel)
    {
        var result = await this.projectCommandService
            .ExecuteAsync(viewModel);

        if (result.Status is ResultStatus.Success)
        {
            return RedirectToAction(nameof(Index));
        }

        if (result.Status is ResultStatus.Invalid)
        {
            ModelState.AddModelErrors(result.Errors);

            return View(viewModel);
        }

        return BadRequest();
    }

    [HttpGet]
    public async Task<IActionResult> Update(Guid id)
    {
        var project = await this.projectQueryService
            .QueryByIdAsync(id);

        if (project is null || !project.IsActive)
        {
            return BadRequest();
        }

        var viewModel = new UpdateProjectViewModel
        {
            Name = project.Name
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(UpdateProjectViewModel viewModel)
    {
        var result = await this.projectCommandService
            .ExecuteAsync(viewModel);

        if (result.Status is ResultStatus.Success)
        {
            return RedirectToAction(nameof(Index));
        }

        if (result.Status is ResultStatus.Invalid)
        {
            ModelState.AddModelErrors(result.Errors);

            return View(viewModel);
        }

        return BadRequest();
    }

    [HttpGet]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var project = await this.projectQueryService
            .QueryByIdAsync(id);

        if (project is null)
        {
            return BadRequest();
        }

        var viewModel = new DeleteProjectViewModel
        {
            Id = project.Id,
            Name = project.Name
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> Delete(DeleteProjectViewModel viewModel)
    {
        var result = await this.projectCommandService
            .ExecuteAsync(viewModel);

        if (result.Status is ResultStatus.Success)
        {
            return RedirectToAction(nameof(Index));
        }

        if (result.Status is ResultStatus.Invalid)
        {
            ModelState.AddModelErrors(result.Errors);

            return View(viewModel);
        }

        return BadRequest();
    }

    [HttpGet]
    public async Task<IActionResult> Deactivate(Guid id)
    {
        var project = await this.projectQueryService
            .QueryByIdAsync(id);

        if (project is null || !project.IsActive)
        {
            return BadRequest();
        }

        var viewModel = new DeactivateProjectViewModel
        {
            Id = project.Id,
            Name = project.Name
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Deactivate(DeactivateProjectViewModel viewModel)
    {
        var result = await this.projectCommandService
            .ExecuteAsync(viewModel);

        if (result.Status is ResultStatus.Success)
        {
            return RedirectToAction(nameof(Index));
        }

        if (result.Status is ResultStatus.Invalid)
        {
            ModelState.AddModelErrors(result.Errors);

            return View(viewModel);
        }

        return BadRequest();
    }

    [HttpGet]
    public async Task<IActionResult> Activate(Guid id)
    {
        var project = await this.projectQueryService
            .QueryByIdAsync(id);

        if (project is null || project.IsActive)
        {
            return BadRequest();
        }

        var viewModel = new ActivateProjectViewModel
        {
            Id = project.Id,
            Name = project.Name
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Activate(ActivateProjectViewModel viewModel)
    {
        var result = await this.projectCommandService
            .ExecuteAsync(viewModel);

        if (result.Status is ResultStatus.Success)
        {
            return RedirectToAction(nameof(Index));
        }

        if (result.Status is ResultStatus.Invalid)
        {
            ModelState.AddModelErrors(result.Errors);

            return View(viewModel);
        }

        return BadRequest();
    }
}
