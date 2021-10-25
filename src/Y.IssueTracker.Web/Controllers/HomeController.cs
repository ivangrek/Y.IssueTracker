namespace Y.IssueTracker.Web.Controllers
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Categories.Domain;
    using Infrastructure;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Priorities.Domain;
    using Projects.Domain;
    using Users.Domain;

    public sealed class HomeController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;

        public HomeController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<IActionResult> Index()
        {
            var isInit = this.applicationDbContext
                .Projects.Any();

            if (!isInit)
            {
                await HttpContext
                    .SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                this.applicationDbContext
                    .Projects.Add(new Project(Guid.NewGuid())
                    {
                        Name = "First project",
                        IsActive = true
                    });

                this.applicationDbContext
                    .Projects.Add(new Project(Guid.NewGuid())
                    {
                        Name = "Second project",
                        IsActive = true
                    });

                this.applicationDbContext
                    .Categories.Add(new Category(Guid.NewGuid())
                    {
                        Name = "Bug",
                        IsActive = true
                    });

                this.applicationDbContext
                    .Categories.Add(new Category(Guid.NewGuid())
                    {
                        Name = "Task",
                        IsActive = true
                    });

                this.applicationDbContext
                    .Priorities.Add(new Priority(Guid.NewGuid())
                    {
                        Name = "High",
                        Weight = 1,
                        IsActive = true
                    });

                this.applicationDbContext
                    .Priorities.Add(new Priority(Guid.NewGuid())
                    {
                        Name = "Medium",
                        Weight = 2,
                        IsActive = true
                    });

                this.applicationDbContext
                    .Priorities.Add(new Priority(Guid.NewGuid())
                    {
                        Name = "Low",
                        Weight = 3,
                        IsActive = true
                    });

                this.applicationDbContext
                    .Users.Add(new User(Users.Domain.User.AdministratorId)
                    {
                        Name = "Administrator",
                        Role = Role.Administrator,
                        IsActive = true
                    });

                this.applicationDbContext
                    .Users.Add(new User(Users.Domain.User.ManagerId)
                    {
                        Name = "Manager",
                        Role = Role.Manager,
                        IsActive = true
                    });

                this.applicationDbContext
                    .Users.Add(new User(Users.Domain.User.UserId)
                    {
                        Name = "User",
                        Role = Role.User,
                        IsActive = true
                    });

                await this.applicationDbContext
                    .SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
