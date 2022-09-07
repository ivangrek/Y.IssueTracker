namespace Y.IssueTracker.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using Y.IssueTracker.Priorities.Commands;
using Y.IssueTracker.Priorities;
using Y.IssueTracker.Priorities.Queries;

[ApiController]
[Route("api/[controller]")]
public class PriorityController : ControllerBase
{
    private readonly IPriorityQueryService priorityQueryService;
    private readonly IPriorityCommandService priorityCommandService;

    public PriorityController(
        IPriorityQueryService priorityQueryService,
        IPriorityCommandService priorityCommandService)
    {
        this.priorityQueryService = priorityQueryService;
        this.priorityCommandService = priorityCommandService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] GetAllQuery query)
    {
        var result = await this.priorityQueryService
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

        var result = await this.priorityQueryService
            .HandleAsync(query);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateCommand command)
    {
        var result = await this.priorityCommandService
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
        var result = await this.priorityCommandService
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

    [HttpPut("Activate")]
    public async Task<IActionResult> ActivateAsync(ActivateCommand command)
    {
        var result = await this.priorityCommandService
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

    [HttpPut("Deactivate")]
    public async Task<IActionResult> DeactivateAsync(DeactivateCommand command)
    {
        var result = await this.priorityCommandService
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
        var result = await this.priorityCommandService
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
