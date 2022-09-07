namespace Y.IssueTracker.Web.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Y.IssueTracker.Web.Models.Category;
using Y.IssueTracker.Categories.Commands;
using Y.IssueTracker.Categories.Queries;
using Y.IssueTracker.Web.Services;
using Y.IssueTracker.Web.Infrastructure;

[Authorize(Roles = "Administrator,Manager")]
public sealed class CategoryController : Controller
{
    private readonly ICategoryService categoryService;

    public CategoryController(
        ICategoryService categoryService)
    {
        this.categoryService = categoryService;
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

        var result = await this.categoryService
            .HandleAsync(query);

        return View(result);
    }

    [HttpGet]
    public IActionResult Create()
    {
        var viewModel = new CreateCategoryViewModel();

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateAsync(CreateCategoryViewModel viewModel)
    {
        var command = new CreateCommand
        {
            Name = viewModel.Name
        };

        var result = await this.categoryService
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

        var result = await this.categoryService
            .HandleAsync(query);

        if (result is null || !result.IsActive)
        {
            return NotFound();
        }

        var viewModel = new UpdateCategoryViewModel
        {
            Name = result.Name
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateAsync(UpdateCategoryViewModel viewModel)
    {
        var command = new UpdateCommand
        {
            Id = viewModel.Id,
            Name = viewModel.Name
        };

        var result = await this.categoryService
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

        var result = await this.categoryService
            .HandleAsync(query);

        if (result is null || result.IsActive)
        {
            return NotFound();
        }

        var viewModel = new ActivateCategoryViewModel
        {
            Id = result.Id,
            Name = result.Name
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ActivateAsync(ActivateCategoryViewModel viewModel)
    {
        var command = new ActivateCommand
        {
            Id = viewModel.Id
        };

        var result = await this.categoryService
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

        var result = await this.categoryService
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

        var result = await this.categoryService
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

        var result = await this.categoryService
            .HandleAsync(query);

        if (result is null)
        {
            return NotFound();
        }

        var viewModel = new DeleteCategoryViewModel
        {
            Id = result.Id,
            Name = result.Name
        };

        return View(viewModel);
    }

    [HttpPost]
    [Authorize(Roles = "Administrator")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteAsync(DeleteCategoryViewModel viewModel)
    {
        var command = new DeleteCommand
        {
            Id = viewModel.Id
        };

        var result = await this.categoryService
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
