namespace Y.IssueTracker.Web.Services;

using System.Net;
using Y.IssueTracker.Priorities.Commands;
using Y.IssueTracker.Priorities.Queries;
using Y.IssueTracker.Priorities.Results;
using Y.IssueTracker.Web.Infrastructure;

public interface IPriorityService
{
    Task<PriorityResult[]> HandleAsync(GetAllQuery query);

    Task<PriorityResult> HandleAsync(GetByIdQuery query);

    Task<IResult> HandleAsync(CreateCommand command);

    Task<IResult> HandleAsync(UpdateCommand command);

    Task<IResult> HandleAsync(ActivateCommand command);

    Task<IResult> HandleAsync(DeactivateCommand command);

    Task<IResult> HandleAsync(DeleteCommand command);
}

internal sealed class PriorityService : IPriorityService
{
    private readonly HttpClient httpClient;

    public PriorityService(IHttpClientFactory httpClientFactory)
    {
        this.httpClient = httpClientFactory.CreateClient("api");
    }

    public async Task<PriorityResult[]> HandleAsync(GetAllQuery query)
    {
        var response = await this.httpClient
            .GetAsync($"Priority?page={query.Page}&pageCount={query.PageCount}");

        response.EnsureSuccessStatusCode();

        return await response.Content
            .ReadAsAsync<PriorityResult[]>();
    }

    public async Task<PriorityResult> HandleAsync(GetByIdQuery query)
    {
        var response = await this.httpClient
            .GetAsync($"Priority/{query.Id}");

        response.EnsureSuccessStatusCode();

        return await response.Content
            .ReadAsAsync<PriorityResult>();
    }

    public async Task<IResult> HandleAsync(CreateCommand command)
    {
        var response = await this.httpClient
            .PostAsJsonAsync("Priority", command);

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
            .PutAsJsonAsync("Priority", command);

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
            .PutAsJsonAsync("Priority/Activate", command);

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
            .PutAsJsonAsync("Priority/Deactivate", command);

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
            .DeleteAsJsonAsync("Priority", command);

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
