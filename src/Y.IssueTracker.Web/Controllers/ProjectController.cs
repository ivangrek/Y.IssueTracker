namespace Y.IssueTracker.Web.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Project;
using Y.IssueTracker.Projects.Commands;
using Y.IssueTracker.Projects.Queries;
using Y.IssueTracker.Web.Infrastructure;
using Y.IssueTracker.Web.Services;

[Authorize(Roles = "Administrator,Manager")]
public sealed class ProjectController : Controller
{
    private readonly IProjectService projectService;

    public ProjectController(
        IProjectService projectService)
    {
        this.projectService = projectService;
    }

    [HttpGet]
    public async Task<IActionResult> IndexAsync(int? page, int? pageCount)
    {
        if (page < 1 || pageCount < 1)
        {
            return NotFound();
        }

        var query = new GetAllQuery
        {
            Page = page ?? 1,
            PageCount = pageCount ?? int.MaxValue
        };

        var result = await this.projectService
            .HandleAsync(query);

        return View(result);
    }

    [HttpGet]
    public IActionResult Create()
    {
        var viewModel = new CreateProjectViewModel();

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateAsync(CreateProjectViewModel viewModel)
    {
        var command = new CreateCommand
        {
            Name = viewModel.Name
        };

        var result = await this.projectService
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

    [HttpGet]
    public async Task<IActionResult> UpdateAsync(Guid id)
    {
        var query = new GetByIdQuery
        {
            Id = id
        };

        var result = await this.projectService
            .HandleAsync(query);

        if (result is null || !result.IsActive)
        {
            return NotFound();
        }

        var viewModel = new UpdateProjectViewModel
        {
            Name = result.Name
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateAsync(UpdateProjectViewModel viewModel)
    {
        var command = new UpdateCommand
        {
            Id = viewModel.Id,
            Name = viewModel.Name
        };

        var result = await this.projectService
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

    [HttpGet]
    public async Task<IActionResult> ActivateAsync(Guid id)
    {
        var query = new GetByIdQuery
        {
            Id = id
        };

        var result = await this.projectService
            .HandleAsync(query);

        if (result is null || result.IsActive)
        {
            return NotFound();
        }

        var viewModel = new ActivateProjectViewModel
        {
            Id = result.Id,
            Name = result.Name
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ActivateAsync(ActivateProjectViewModel viewModel)
    {
        var command = new ActivateCommand
        {
            Id = viewModel.Id
        };

        var result = await this.projectService
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

    [HttpGet]
    public async Task<IActionResult> DeactivateAsync(Guid id)
    {
        var query = new GetByIdQuery
        {
            Id = id
        };

        var result = await this.projectService
            .HandleAsync(query);

        if (result is null || !result.IsActive)
        {
            return NotFound();
        }

        var viewModel = new DeactivateProjectViewModel
        {
            Id = result.Id,
            Name = result.Name
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeactivateAsync(DeactivateProjectViewModel viewModel)
    {
        var command = new DeactivateCommand
        {
            Id = viewModel.Id
        };

        var result = await this.projectService
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

    [HttpGet]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var query = new GetByIdQuery
        {
            Id = id
        };

        var result = await this.projectService
            .HandleAsync(query);

        if (result is null)
        {
            return NotFound();
        }

        var viewModel = new DeleteProjectViewModel
        {
            Id = result.Id,
            Name = result.Name
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeleteAsync(DeleteProjectViewModel viewModel)
    {
        var command = new DeleteCommand
        {
            Id = viewModel.Id
        };

        var result = await this.projectService
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
}
