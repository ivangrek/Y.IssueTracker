namespace Y.IssueTracker.Web.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Priority;
using Y.IssueTracker.Priorities.Commands;
using Y.IssueTracker.Priorities.Queries;
using Y.IssueTracker.Web.Infrastructure;
using Y.IssueTracker.Web.Services;

[Authorize(Roles = "Administrator,Manager")]
public sealed class PriorityController : Controller
{
    private readonly IPriorityService priorityService;

    public PriorityController(
        IPriorityService priorityService)
    {
        this.priorityService = priorityService;
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

        var result = await this.priorityService
            .HandleAsync(query);

        return View(result);
    }

    [HttpGet]
    public IActionResult Create()
    {
        var viewModel = new CreatePriorityViewModel();

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateAsync(CreatePriorityViewModel viewModel)
    {
        var command = new CreateCommand
        {
            Name = viewModel.Name,
            Weight = viewModel.Weight
        };

        var result = await this.priorityService
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

        var result = await this.priorityService
            .HandleAsync(query);

        if (result is null || !result.IsActive)
        {
            return NotFound();
        }

        var viewModel = new UpdatePriorityViewModel
        {
            Name = result.Name,
            Weight = result.Weight
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateAsync(UpdatePriorityViewModel viewModel)
    {
        var command = new UpdateCommand
        {
            Id = viewModel.Id,
            Name = viewModel.Name,
            Weight = viewModel.Weight
        };

        var result = await this.priorityService
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

        var result = await this.priorityService
            .HandleAsync(query);

        if (result is null || result.IsActive)
        {
            return NotFound();
        }

        var viewModel = new ActivatePriorityViewModel
        {
            Id = result.Id,
            Name = result.Name
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ActivateAsync(ActivatePriorityViewModel viewModel)
    {
        var command = new ActivateCommand
        {
            Id = viewModel.Id
        };

        var result = await this.priorityService
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

        var result = await this.priorityService
            .HandleAsync(query);

        if (result is null || !result.IsActive)
        {
            return NotFound();
        }

        var viewModel = new DeactivateCategoryViewModel
        {
            Id = result.Id,
            Name = result.Name
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeactivateAsync(DeactivateCategoryViewModel viewModel)
    {
        var command = new DeactivateCommand
        {
            Id = viewModel.Id
        };

        var result = await this.priorityService
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

        var result = await this.priorityService
            .HandleAsync(query);

        if (result is null)
        {
            return NotFound();
        }

        var viewModel = new DeletePriorityViewModel
        {
            Id = result.Id,
            Name = result.Name
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeleteAsync(DeletePriorityViewModel viewModel)
    {
        var command = new DeleteCommand
        {
            Id = viewModel.Id
        };

        var result = await this.priorityService
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
