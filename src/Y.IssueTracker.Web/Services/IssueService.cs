namespace Y.IssueTracker.Web.Services;

using System.Net;
using Y.IssueTracker.Issues.Commands;
using Y.IssueTracker.Issues.Queries;
using Y.IssueTracker.Issues.Results;
using Y.IssueTracker.Web.Infrastructure;

public interface IIssueService
{
    Task<IssueForListItemResult[]> HandleAsync(GetIssuesForListQuery query);

    Task<IssueResult> HandleAsync(GetByIdQuery query);

    Task<IssueForViewResult> HandleAsync(GetIssueForViewQuery query);

    Task<IResult> HandleAsync(CreateCommand command);

    Task<IResult> HandleAsync(UpdateCommand command);

    Task<IResult> HandleAsync(DeleteCommand command);
}

internal sealed class IssueService : IIssueService
{
    private readonly HttpClient httpClient;

    public IssueService(IHttpClientFactory httpClientFactory)
    {
        this.httpClient = httpClientFactory.CreateClient("api");
    }

    public async Task<IssueForListItemResult[]> HandleAsync(GetIssuesForListQuery query)
    {
        var response = await this.httpClient
            .GetAsync($"Issue?page={query.Page}&pageCount={query.PageCount}");

        response.EnsureSuccessStatusCode();

        return await response.Content
            .ReadAsAsync<IssueForListItemResult[]>();
    }

    public async Task<IssueResult> HandleAsync(GetByIdQuery query)
    {
        var response = await this.httpClient
            .GetAsync($"Issue/{query.Id}");

        response.EnsureSuccessStatusCode();

        return await response.Content
            .ReadAsAsync<IssueResult>();
    }

    public async Task<IssueForViewResult> HandleAsync(GetIssueForViewQuery query)
    {
        var response = await this.httpClient
            .GetAsync($"Issue/{query.Id}/View");

        response.EnsureSuccessStatusCode();

        return await response.Content
            .ReadAsAsync<IssueForViewResult>();
    }

    public async Task<IResult> HandleAsync(CreateCommand command)
    {
        var response = await this.httpClient
            .PostAsJsonAsync("Issue", command);

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
            .PutAsJsonAsync("Issue", command);

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
            .DeleteAsJsonAsync("Issue", command);

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
