namespace Y.IssueTracker.Web.Controllers;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Account;
using Y.IssueTracker.Users;

public sealed class AccountController : Controller
{
    private readonly IUserCommandService userCommandService;

    public AccountController(
        IUserCommandService userCommandService)
    {
        this.userCommandService = userCommandService;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel viewModel)
    {
        var result = await this.userCommandService
            .ExecuteAsync(viewModel);

        if (result.Status is ResultStatus.Success)
        {
            return RedirectToAction("Index", "Home");
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

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await HttpContext
            .SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return RedirectToAction("Index", "Home");
    }
}
