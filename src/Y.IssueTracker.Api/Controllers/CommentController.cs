namespace Y.IssueTracker.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using Y.IssueTracker.Comments;
using Y.IssueTracker.Comments.Commands;
using Y.IssueTracker.Comments.Queries;

[ApiController]
[Route("api/[controller]")]
public class CommentController : ControllerBase
{
    private readonly ICommentQueryService commentQueryService;
    private readonly ICommentCommandService commentCommandService;

    public CommentController(
        ICommentQueryService commentQueryService,
        ICommentCommandService commentCommandService)
    {
        this.commentQueryService = commentQueryService;
        this.commentCommandService = commentCommandService;
    }

    [HttpGet("{issueId}")]
    public async Task<IActionResult> GetCommentsForViewAsync(Guid issueId)
    {
        var query = new GetCommentsForViewQuery
        {
            IssueId = issueId
        };

        var result = await this.commentQueryService
            .HandleAsync(query);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateCommand command)
    {
        var result = await this.commentCommandService
            .HandleAsync(command);

        if (result.Status is ResultStatus.Success)
        {
            return Ok();
        }

        if (result.Status is ResultStatus.Invalid)
        {
            return BadRequest(result.Errors);
        }

        return StatusCode(StatusCodes.Status500InternalServerError, new
        {
            message = "Internal server error."
        });
    }
}
