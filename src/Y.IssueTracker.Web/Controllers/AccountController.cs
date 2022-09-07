namespace Y.IssueTracker.Web.Controllers;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Account;
using Y.IssueTracker.Users.Commands;
using Y.IssueTracker.Web.Infrastructure;
using Y.IssueTracker.Web.Services;

public sealed class AccountController : Controller
{
    private readonly IUserService userService;

    public AccountController(
        IUserService userService)
    {
        this.userService = userService;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> RegisterAsync(RegisterViewModel viewModel)
    //{
    //    var command = new RegisterCommand
    //    {
    //        Email = viewModel.Email,
    //        Password = viewModel.Password,
    //        PasswordConfirm = viewModel.PasswordConfirm
    //    };

    //    var result = await this.userService
    //        .HandleAsync(command);

    //    if (result.Status is ResultStatus.Success)
    //    {
    //        return RedirectToAction(nameof(Login));
    //    }

    //    if (result.Status is ResultStatus.Invalid)
    //    {
    //        ModelState.AddModelErrors(result.Errors);

    //        return View(viewModel);
    //    }

    //    return StatusCode(StatusCodes.Status500InternalServerError);
    //}

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> LoginAsync(LoginViewModel viewModel)
    {
        var command = new LoginCommand
        {
            Email = viewModel.Email,
            Password = viewModel.Password,
            RememberMe = viewModel.RememberMe
        };

        var result = await this.userService
            .HandleAsync(command);

        if (result.Status is ResultStatus.Success)
        {
            return RedirectToAction("Index", "Home");
        }

        if (result.Status is ResultStatus.Invalid)
        {
            ModelState.AddModelErrors(result.Errors);

            return View(viewModel);
        }

        return StatusCode(StatusCodes.Status500InternalServerError);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public async Task<IActionResult> LogoutAsync(LogoutViewModel viewModel)
    {
        var command = new LogoutCommand();
        var result = await this.userService
            .HandleAsync(command);

        if (result.Status is ResultStatus.Success)
        {
            return RedirectToAction("Index", "Home");
        }

        if (result.Status is ResultStatus.Invalid)
        {
            ModelState.AddModelErrors(result.Errors);

            return View(viewModel);
        }

        return StatusCode(StatusCodes.Status500InternalServerError);
    }

    [HttpGet]
    public IActionResult ResetPassword()
    {
        return View();
    }

    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> ResetPasswordAsync(ResetPasswordViewModel viewModel)
    //{
    //    var command = new ResetPasswordCommand
    //    {
    //        Email = viewModel.Email
    //    };

    //    var result = await this.userService
    //        .HandleAsync(command);

    //    if (result.Status is ResultStatus.Success)
    //    {
    //        return RedirectToAction(nameof(Login));
    //    }

    //    if (result.Status is ResultStatus.Invalid)
    //    {
    //        ModelState.AddModelErrors(result.Errors);

    //        return View(viewModel);
    //    }

    //    return StatusCode(StatusCodes.Status500InternalServerError);
    //}
}
