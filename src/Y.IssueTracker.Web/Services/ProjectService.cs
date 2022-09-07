namespace Y.IssueTracker.Web.Services;

using System.Net;
using Y.IssueTracker.Projects.Commands;
using Y.IssueTracker.Projects.Queries;
using Y.IssueTracker.Projects.Results;
using Y.IssueTracker.Web.Infrastructure;

public interface IProjectService
{
    Task<ProjectResult[]> HandleAsync(GetAllQuery query);

    Task<ProjectResult> HandleAsync(GetByIdQuery query);

    Task<IResult> HandleAsync(CreateCommand command);

    Task<IResult> HandleAsync(UpdateCommand command);

    Task<IResult> HandleAsync(ActivateCommand command);

    Task<IResult> HandleAsync(DeactivateCommand command);

    Task<IResult> HandleAsync(DeleteCommand command);
}

internal sealed class ProjectService : IProjectService
{
    private readonly HttpClient httpClient;

    public ProjectService(IHttpClientFactory httpClientFactory)
    {
        this.httpClient = httpClientFactory.CreateClient("api");
    }

    public async Task<ProjectResult[]> HandleAsync(GetAllQuery query)
    {
        var response = await this.httpClient
            .GetAsync($"Project?page={query.Page}&pageCount={query.PageCount}");

        response.EnsureSuccessStatusCode();

        return await response.Content
            .ReadAsAsync<ProjectResult[]>();
    }

    public async Task<ProjectResult> HandleAsync(GetByIdQuery query)
    {
        var response = await this.httpClient
            .GetAsync($"Project/{query.Id}");

        response.EnsureSuccessStatusCode();

        return await response.Content
            .ReadAsAsync<ProjectResult>();
    }

    public async Task<IResult> HandleAsync(CreateCommand command)
    {
        var response = await this.httpClient
            .PostAsJsonAsync("Project", command);

        if (response.IsSuccessStatusCode)
        {
            return Result.Success();
        }

        if (response.StatusCode is HttpStatusCode.BadRequest)
        {
            var errors = await response.Content.ReadAsAsync<KeyValuePair<string, string>[]>();

            return Result.Invalid()
                .WithErrors(errors)
                .Build();
        }

        return Result.Failure()
            .Build();
    }

    public async Task<IResult> HandleAsync(UpdateCommand command)
    {
        var response = await this.httpClient
            .PutAsJsonAsync("Project", command);

        if (response.IsSuccessStatusCode)
        {
            return Result.Success();
        }

        if (response.StatusCode is HttpStatusCode.BadRequest)
        {
            var errors = await response.Content.ReadAsAsync<KeyValuePair<string, string>[]>();

            return Result.Invalid()
                .WithErrors(errors)
                .Build();
        }

        return Result.Failure()
            .Build();
    }

    public async Task<IResult> HandleAsync(ActivateCommand command)
    {
        var response = await this.httpClient
            .PutAsJsonAsync("Project/Activate", command);

        if (response.IsSuccessStatusCode)
        {
            return Result.Success();
        }

        if (response.StatusCode is HttpStatusCode.BadRequest)
        {
            var errors = await response.Content.ReadAsAsync<KeyValuePair<string, string>[]>();

            return Result.Invalid()
                .WithErrors(errors)
                .Build();
        }

        return Result.Failure()
            .Build();
    }

    public async Task<IResult> HandleAsync(DeactivateCommand command)
    {
        var response = await this.httpClient
            .PutAsJsonAsync("Project/Deactivate", command);

        if (response.IsSuccessStatusCode)
        {
            return Result.Success();
        }

        if (response.StatusCode is HttpStatusCode.BadRequest)
        {
            var errors = await response.Content.ReadAsAsync<KeyValuePair<string, string>[]>();

            return Result.Invalid()
                .WithErrors(errors)
                .Build();
        }

        return Result.Failure()
            .Build();
    }

    public async Task<IResult> HandleAsync(DeleteCommand command)
    {
        var response = await this.httpClient
            .DeleteAsJsonAsync("Project", command);

        if (response.IsSuccessStatusCode)
        {
            return Result.Success();
        }

        if (response.StatusCode is HttpStatusCode.BadRequest)
        {
            var errors = await response.Content.ReadAsAsync<KeyValuePair<string, string>[]>();

            return Result.Invalid()
                .WithErrors(errors)
                .Build();
        }

        return Result.Failure()
            .Build();
    }
}
