namespace Y.IssueTracker.Web.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.User;
using Users;
using Y.IssueTracker.Web.Infrastructure;

[Authorize]
public sealed class UserController : Controller
{
    private readonly IUserCommandService userCommandService;
    private readonly IUserQueryService userQueryService;

    public UserController(
        IUserCommandService userCommandService,
        IUserQueryService userQueryService)
    {
        this.userCommandService = userCommandService;
        this.userQueryService = userQueryService;
    }

    [HttpGet]
    [Authorize(Roles = "Administrator,Manager")]
    public async Task<IActionResult> Index()
    {
        var users = await this.userQueryService
            .QueryAllAsync();

        return View(users);
    }

    [HttpGet]
    public async Task<IActionResult> View(Guid id)
    {
        var user = await this.userQueryService
            .QueryByIdAsync(id);

        return View(user);
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
    public async Task<IActionResult> Create(CreateUserViewModel viewModel)
    {
        var result = await this.userCommandService
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
    public async Task<IActionResult> Update(Guid id)
    {
        var user = await this.userQueryService
            .QueryByIdAsync(id);

        if (user is null || !user.IsActive)
        {
            return BadRequest();
        }

        var viewModel = new UpdateUserViewModel
        {
            Name = user.Name,
            Role = user.Role
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> Update(UpdateUserViewModel viewModel)
    {
        var result = await this.userCommandService
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
        var user = await this.userQueryService
            .QueryByIdAsync(id);

        if (user is null || user.IsDefault)
        {
            return BadRequest();
        }

        var viewModel = new DeleteUserViewModel
        {
            Id = user.Id,
            Name = user.Name
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> Delete(DeleteUserViewModel viewModel)
    {
        var result = await this.userCommandService
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
    [Authorize(Roles = "Administrator,Manager")]
    public async Task<IActionResult> Deactivate(Guid id)
    {
        var user = await this.userQueryService
            .QueryByIdAsync(id);

        if (user is null || !user.IsActive || user.IsDefault)
        {
            return BadRequest();
        }

        var viewModel = new DeactivateUserViewModel
        {
            Id = user.Id,
            Name = user.Name
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrator,Manager")]
    public async Task<IActionResult> Deactivate(DeactivateUserViewModel viewModel)
    {
        var result = await this.userCommandService
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
    [Authorize(Roles = "Administrator,Manager")]
    public async Task<IActionResult> Activate(Guid id)
    {
        var user = await this.userQueryService
            .QueryByIdAsync(id);

        if (user is null || user.IsActive || user.IsDefault)
        {
            return BadRequest();
        }

        var viewModel = new ActivateUserViewModel
        {
            Id = user.Id,
            Name = user.Name
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrator,Manager")]
    public async Task<IActionResult> Activate(ActivateUserViewModel viewModel)
    {
        var result = await this.userCommandService
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
