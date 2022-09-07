namespace Y.IssueTracker.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using Y.IssueTracker.Categories;
using Y.IssueTracker.Categories.Commands;
using Y.IssueTracker.Categories.Queries;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryQueryService categoryQueryService;
    private readonly ICategoryCommandService categoryCommandService;

    public CategoryController(
        ICategoryQueryService categoryQueryService,
        ICategoryCommandService categoryCommandService)
    {
        this.categoryQueryService = categoryQueryService;
        this.categoryCommandService = categoryCommandService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] GetAllQuery query)
    {
        var result = await this.categoryQueryService
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

        var result = await this.categoryQueryService
            .HandleAsync(query);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateCommand command)
    {
        var result = await this.categoryCommandService
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
        var result = await this.categoryCommandService
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
        var result = await this.categoryCommandService
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
        var result = await this.categoryCommandService
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
        var result = await this.categoryCommandService
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
