namespace Y.IssueTracker.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Infrastructure;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Models.Account;
    using Users.Domain;

    public sealed class AccountController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;

        public AccountController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
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
            User user = null;

            switch (viewModel.Email)
            {
                case "user" when viewModel.Password == "user":
                    user = await this.applicationDbContext
                        .Users.SingleAsync(x => x.Id == Users.Domain.User.UserId);
                    break;
                case "manager" when viewModel.Password == "manager":
                    user = await this.applicationDbContext
                        .Users.SingleAsync(x => x.Id == Users.Domain.User.ManagerId);
                    break;
                case "admin" when viewModel.Password == "admin":
                    user = await this.applicationDbContext
                        .Users.SingleAsync(x => x.Id == Users.Domain.User.AdministratorId);
                    break;
            }

            if (user is null)
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password.");

                return View();
            }

            await DoLogin(user, viewModel.RememberMe);

            return RedirectToAction("Index", "Home");
        }

        private Task DoLogin(User user, bool rememberMe)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            var properties = new AuthenticationProperties
            {
                IsPersistent = rememberMe,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(60)
            };

            return HttpContext
                .SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, properties);
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
}
