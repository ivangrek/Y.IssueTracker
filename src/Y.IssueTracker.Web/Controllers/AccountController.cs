namespace Y.IssueTracker.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
            var user = await this.applicationDbContext
                .Users
                .Where(x => viewModel.Email == viewModel.Password)
                .Where(x => x.Name == viewModel.Email)
                .Where(x => x.IsActive)
                .SingleOrDefaultAsync();

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
