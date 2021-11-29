namespace Y.IssueTracker.Web.Controllers;

using System;
using System.Threading.Tasks;
using Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Category;

[Authorize(Roles = "Administrator,Manager")]
public sealed class CategoryController : Controller
{
    private readonly ICategoryCommandService categoryCommandService;
    private readonly ICategoryQueryService categoryQueryService;

    public CategoryController(
        ICategoryCommandService categoryCommandService,
        ICategoryQueryService categoryQueryService)
    {
        this.categoryCommandService = categoryCommandService;
        this.categoryQueryService = categoryQueryService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var categories = await this.categoryQueryService
            .QueryAllAsync();

        return View(categories);
    }

    [HttpGet]
    public IActionResult Create()
    {
        var viewModel = new CreateCategoryViewModel();

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateCategoryViewModel viewModel)
    {
        var result = await this.categoryCommandService
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

    [HttpGet]
    public async Task<IActionResult> Update(Guid id)
    {
        var category = await this.categoryQueryService
            .QueryByIdAsync(id);

        if (category is null || !category.IsActive)
        {
            return BadRequest();
        }

        var viewModel = new UpdateCategoryViewModel
        {
            Name = category.Name
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(UpdateCategoryViewModel viewModel)
    {
        var result = await this.categoryCommandService
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

    [HttpGet]
    public async Task<IActionResult> Delete(Guid id)
    {
        var category = await this.categoryQueryService
            .QueryByIdAsync(id);

        if (category is null)
        {
            return BadRequest();
        }

        var viewModel = new DeleteCategoryViewModel
        {
            Id = category.Id,
            Name = category.Name
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> Delete(DeleteCategoryViewModel viewModel)
    {
        var result = await this.categoryCommandService
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

    [HttpGet]
    public async Task<IActionResult> Deactivate(Guid id)
    {
        var category = await this.categoryQueryService
            .QueryByIdAsync(id);

        if (category is null || !category.IsActive)
        {
            return BadRequest();
        }

        var viewModel = new DeactivateCategoryViewModel
        {
            Id = category.Id,
            Name = category.Name
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Deactivate(DeactivateCategoryViewModel viewModel)
    {
        var result = await this.categoryCommandService
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

    [HttpGet]
    public async Task<IActionResult> Activate(Guid id)
    {
        var category = await this.categoryQueryService
            .QueryByIdAsync(id);

        if (category is null || category.IsActive)
        {
            return BadRequest();
        }

        var viewModel = new ActivateCategoryViewModel
        {
            Id = category.Id,
            Name = category.Name
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> Activate(ActivateCategoryViewModel viewModel)
    {
        var result = await this.categoryCommandService
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
}
