namespace Y.IssueTracker.Web.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.User;
using Y.IssueTracker.Users.Commands;
using Y.IssueTracker.Users.Queries;
using Y.IssueTracker.Web.Infrastructure;
using Y.IssueTracker.Web.Services;

[Authorize]
public sealed class UserController : Controller
{
    private readonly IUserService userService;

    public UserController(
        IUserService userService)
    {
        this.userService = userService;
    }

    [HttpGet]
    [Authorize(Roles = "Administrator,Manager")]
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

        var result = await this.userService
            .HandleAsync(query);

        return View(result);
    }

    [HttpGet]
    public async Task<IActionResult> ViewAsync(Guid id)
    {
        var query = new GetByIdQuery
        {
            Id = id
        };

        var result = await this.userService
            .HandleAsync(query);

        return View(result);
    }

    [HttpGet]
    [Authorize(Roles = "Administrator")]
    public IActionResult Create()
    {
        var viewModel = new CreateUserViewModel();

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> CreateAsync(CreateUserViewModel viewModel)
    {
        var command = new CreateCommand
        {
            Name = viewModel.Name,
            Role = viewModel.Role
        };

        var result = await this.userService
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
    public async Task<IActionResult> UpdateAsync(Guid id)
    {
        var query = new GetByIdQuery
        {
            Id = id
        };

        var result = await this.userService
            .HandleAsync(query);

        if (result is null || !result.IsActive)
        {
            return NotFound();
        }

        var viewModel = new UpdateUserViewModel
        {
            Name = result.Name,
            Role = result.Role
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> UpdateAsync(UpdateUserViewModel viewModel)
    {
        var command = new UpdateCommand
        {
            Id = viewModel.Id,
            Name = viewModel.Name,
            Role = viewModel.Role
        };

        var result = await this.userService
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
    [Authorize(Roles = "Administrator,Manager")]
    public async Task<IActionResult> ActivateAsync(Guid id)
    {
        var query = new GetByIdQuery
        {
            Id = id
        };

        var result = await this.userService
            .HandleAsync(query);

        if (result is null || result.IsActive || result.IsDefault)
        {
            return NotFound();
        }

        var viewModel = new ActivateUserViewModel
        {
            Id = result.Id,
            Name = result.Name
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrator,Manager")]
    public async Task<IActionResult> ActivateAsync(ActivateUserViewModel viewModel)
    {
        var command = new ActivateCommand
        {
            Id = viewModel.Id
        };

        var result = await this.userService
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
    [Authorize(Roles = "Administrator,Manager")]
    public async Task<IActionResult> DeactivateAsync(Guid id)
    {
        var query = new GetByIdQuery
        {
            Id = id
        };

        var result = await this.userService
            .HandleAsync(query);

        if (result is null || !result.IsActive || result.IsDefault)
        {
            return NotFound();
        }

        var viewModel = new DeactivateUserViewModel
        {
            Id = result.Id,
            Name = result.Name
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrator,Manager")]
    public async Task<IActionResult> DeactivateAsync(DeactivateUserViewModel viewModel)
    {
        var command = new DeactivateCommand
        {
            Id = viewModel.Id
        };

        var result = await this.userService
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

        var result = await this.userService
            .HandleAsync(query);

        if (result is null || result.IsDefault)
        {
            return NotFound();
        }

        var viewModel = new DeleteUserViewModel
        {
            Id = result.Id,
            Name = result.Name
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeleteAsync(DeleteUserViewModel viewModel)
    {
        var command = new DeleteCommand
        {
            Id = viewModel.Id
        };

        var result = await this.userService
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
