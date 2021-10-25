namespace Y.IssueTracker.Web.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.Priority;
    using Priorities;

    [Authorize(Roles = "Administrator,Manager")]
    public sealed class PriorityController : Controller
    {
        private readonly IPriorityCommandService priorityCommandService;
        private readonly IPriorityQueryService priorityQueryService;

        public PriorityController(
            IPriorityCommandService priorityCommandService,
            IPriorityQueryService priorityQueryService)
        {
            this.priorityQueryService = priorityQueryService;
            this.priorityCommandService = priorityCommandService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var priorities = await this.priorityQueryService
                .QueryAllAsync();

            return View(priorities);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new CreatePriorityViewModel();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreatePriorityViewModel viewModel)
        {
            var result = await this.priorityCommandService
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
            var priority = await this.priorityQueryService
                .QueryByIdAsync(id);

            if (priority is null || !priority.IsActive)
            {
                return BadRequest();
            }

            var viewModel = new UpdatePriorityViewModel
            {
                Name = priority.Name,
                Weight = priority.Weight
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdatePriorityViewModel viewModel)
        {
            var result = await this.priorityCommandService
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
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var priority = await this.priorityQueryService
                .QueryByIdAsync(id);

            if (priority is null)
            {
                return BadRequest();
            }

            var viewModel = new DeletePriorityViewModel
            {
                Id = priority.Id,
                Name = priority.Name
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(DeletePriorityViewModel viewModel)
        {
            var result = await this.priorityCommandService
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
            var priority = await this.priorityQueryService
                .QueryByIdAsync(id);

            if (priority is null || !priority.IsActive)
            {
                return BadRequest();
            }

            var viewModel = new DeactivateCategoryViewModel
            {
                Id = priority.Id,
                Name = priority.Name
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deactivate(DeactivateCategoryViewModel viewModel)
        {
            var result = await this.priorityCommandService
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
            var priority = await this.priorityQueryService
                .QueryByIdAsync(id);

            if (priority is null || priority.IsActive)
            {
                return BadRequest();
            }

            var viewModel = new ActivatePriorityViewModel
            {
                Id = priority.Id,
                Name = priority.Name
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Activate(ActivatePriorityViewModel viewModel)
        {
            var result = await this.priorityCommandService
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
}
