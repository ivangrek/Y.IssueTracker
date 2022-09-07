namespace Y.IssueTracker.Web.Services;

using System.Net;
using Y.IssueTracker.Categories.Commands;
using Y.IssueTracker.Categories.Queries;
using Y.IssueTracker.Categories.Results;
using Y.IssueTracker.Web.Infrastructure;

public interface ICategoryService
{
    Task<CategoryResult[]> HandleAsync(GetAllQuery query);

    Task<CategoryResult> HandleAsync(GetByIdQuery query);

    Task<IResult> HandleAsync(CreateCommand command);

    Task<IResult> HandleAsync(UpdateCommand command);

    Task<IResult> HandleAsync(ActivateCommand command);

    Task<IResult> HandleAsync(DeactivateCommand command);

    Task<IResult> HandleAsync(DeleteCommand command);
}

internal sealed class CategoryService : ICategoryService
{
    private readonly HttpClient httpClient;

    public CategoryService(IHttpClientFactory httpClientFactory)
    {
        this.httpClient = httpClientFactory.CreateClient("api");
    }

    public async Task<CategoryResult[]> HandleAsync(GetAllQuery query)
    {
        var response = await this.httpClient
            .GetAsync($"Category?page={query.Page}&pageCount={query.PageCount}");

        response.EnsureSuccessStatusCode();

        return await response.Content
            .ReadAsAsync<CategoryResult[]>();
    }

    public async Task<CategoryResult> HandleAsync(GetByIdQuery query)
    {
        var response = await this.httpClient
            .GetAsync($"Category/{query.Id}");

        response.EnsureSuccessStatusCode();

        return await response.Content
            .ReadAsAsync<CategoryResult>();
    }

    public async Task<IResult> HandleAsync(CreateCommand command)
    {
        var response = await this.httpClient
            .PostAsJsonAsync("Category", command);

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
            .PutAsJsonAsync("Category", command);

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
            .PutAsJsonAsync("Category/Activate", command);

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
            .PutAsJsonAsync("Category/Deactivate", command);

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
            .DeleteAsJsonAsync("Category", command);

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
