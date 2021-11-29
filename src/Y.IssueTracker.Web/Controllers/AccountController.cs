﻿namespace Y.IssueTracker.Web.Controllers;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Account;
using Y.IssueTracker.Users;
using Y.IssueTracker.Web.Infrastructure;

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
            ModelState.AddModelErrors(result.Errors);

            return View(viewModel);
        }

        return BadRequest();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public async Task<IActionResult> Logout(LogoutViewModel viewModel)
    {
        var result = await this.userCommandService
            .ExecuteAsync(viewModel);

        if (result.Status is ResultStatus.Success)
        {
            return RedirectToAction("Index", "Home");
        }

        if (result.Status is ResultStatus.Invalid)
        {
            ModelState.AddModelErrors(result.Errors);

            return View(viewModel);
        }

        return BadRequest();
    }
}
