namespace Y.IssueTracker.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using Y.IssueTracker.Issues;
using Y.IssueTracker.Issues.Commands;
using Y.IssueTracker.Issues.Queries;

[ApiController]
[Route("api/[controller]")]
public class IssueController : ControllerBase
{
    private readonly IIssueQueryService issueQueryService;
    private readonly IIssueCommandService issueCommandService;

    public IssueController(
        IIssueQueryService issueQueryService,
        IIssueCommandService issueCommandService)
    {
        this.issueQueryService = issueQueryService;
        this.issueCommandService = issueCommandService;
    }

    [HttpGet]
    public async Task<IActionResult> GetIssuesForListAsync([FromQuery] GetIssuesForListQuery query)
    {
        var result = await this.issueQueryService
            .HandleAsync(query);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var query = new GetByIdQuery
        {
            Id = id
        };

        var result = await this.issueQueryService
            .HandleAsync(query);

        return Ok(result);
    }

    [HttpGet("{id}/View")]
    public async Task<IActionResult> GetIssueForViewAsync(Guid id)
    {
        var query = new GetIssueForViewQuery
        {
            Id = id
        };

        var result = await this.issueQueryService
            .HandleAsync(query);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateCommand command)
    {
        var result = await this.issueCommandService
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

    [HttpPut]
    public async Task<IActionResult> UpdateAsync(UpdateCommand command)
    {
        var result = await this.issueCommandService
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

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(DeleteCommand command)
    {
        var result = await this.issueCommandService
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
