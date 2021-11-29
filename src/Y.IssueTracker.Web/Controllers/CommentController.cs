namespace Y.IssueTracker.Web.Controllers;

using System;
using System.Threading.Tasks;
using Comments;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Comment;

[Authorize]
public sealed class CommentController : Controller
{
    private readonly ICommentCommandService commentCommandService;

    public CommentController(ICommentCommandService commentCommandService)
    {
        this.commentCommandService = commentCommandService;
    }

    [HttpGet]
    public IActionResult Create(Guid id)
    {
        var viewModel = new CreateCommentViewModel
        {
            IssueId = id
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateCommentViewModel viewModel)
    {
        viewModel.AuthorUserId = User.GetUserId();

        var result = await this.commentCommandService
            .ExecuteAsync(viewModel);

        if (result.Status is ResultStatus.Success)
        {
            return RedirectToAction("View", "Issue", new { id = viewModel.IssueId });
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
