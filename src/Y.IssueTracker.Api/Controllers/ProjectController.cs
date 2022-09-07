namespace Y.IssueTracker.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using Y.IssueTracker.Projects.Commands;
using Y.IssueTracker.Projects;
using Y.IssueTracker.Projects.Queries;

[ApiController]
[Route("api/[controller]")]
public class ProjectController : ControllerBase
{
    private readonly IProjectQueryService projectQueryService;
    private readonly IProjectCommandService projectCommandService;

    public ProjectController(
        IProjectQueryService projectQueryService,
        IProjectCommandService projectCommandService)
    {
        this.projectQueryService = projectQueryService;
        this.projectCommandService = projectCommandService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] GetAllQuery query)
    {
        var result = await this.projectQueryService
            .HandleAsync(query);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAllByIdAsync(Guid id)
    {
        var query = new GetByIdQuery
        {
            Id = id
        };

        var result = await this.projectQueryService
            .HandleAsync(query);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateCommand command)
    {
        var result = await this.projectCommandService
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
        var result = await this.projectCommandService
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
        var result = await this.projectCommandService
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
        var result = await this.projectCommandService
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
        var result = await this.projectCommandService
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
