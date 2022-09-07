namespace Y.IssueTracker.Web.Controllers;

using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Comment;
using Y.IssueTracker.Comments.Commands;
using Y.IssueTracker.Web.Services;

[Authorize]
public sealed class CommentController : Controller
{
    private readonly ICommentService commentService;

    public CommentController(ICommentService commentService)
    {
        this.commentService = commentService;
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
    public async Task<IActionResult> CreateAsync(CreateCommentViewModel viewModel)
    {
        var command = new CreateCommand
        {
            IssueId = viewModel.IssueId,
            Text = viewModel.Text,
            AuthorUserId = User.GetUserId()
        };

        var result = await this.commentService
            .HandleAsync(command);

        if (result.Status is ResultStatus.Success)
        {
            return RedirectToAction("View", "Issue", new { id = viewModel.IssueId });
        }

        if (result.Status is ResultStatus.Invalid)
        {
            ModelState.AddModelErrors(result.Errors);

            return View(viewModel);
        }

        return StatusCode(StatusCodes.Status500InternalServerError);
    }
}
