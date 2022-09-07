namespace Y.IssueTracker.Web.Services;

using System.Net;
using Y.IssueTracker.Users;
using Y.IssueTracker.Users.Commands;
using Y.IssueTracker.Users.Domain;
using Y.IssueTracker.Users.Queries;
using Y.IssueTracker.Users.Results;
using Y.IssueTracker.Web.Infrastructure;

public interface IUserService
{
    Task<UserResult[]> HandleAsync(GetAllQuery query);

    Task<UserResult> HandleAsync(GetByIdQuery query);

    Task<IResult> HandleAsync(CreateCommand command);

    Task<IResult> HandleAsync(UpdateCommand command);

    Task<IResult> HandleAsync(ActivateCommand command);

    Task<IResult> HandleAsync(DeactivateCommand command);

    Task<IResult> HandleAsync(DeleteCommand command);

    Task<IResult> HandleAsync(LoginCommand command);

    Task<IResult> HandleAsync(LogoutCommand command);
}

internal sealed class UserService : IUserService
{
    private readonly HttpClient httpClient;
    private readonly IAccountService accountService;

    public UserService(
        IHttpClientFactory httpClientFactory,
        IAccountService accountService)
    {
        this.httpClient = httpClientFactory.CreateClient("api");
        this.accountService = accountService;
    }

    public async Task<UserResult[]> HandleAsync(GetAllQuery query)
    {
        var response = await this.httpClient
            .GetAsync($"User?page={query.Page}&pageCount={query.PageCount}");

        response.EnsureSuccessStatusCode();

        return await response.Content
            .ReadAsAsync<UserResult[]>();
    }

    public async Task<UserResult> HandleAsync(GetByIdQuery query)
    {
        var response = await this.httpClient
            .GetAsync($"User/{query.Id}");

        response.EnsureSuccessStatusCode();

        return await response.Content
            .ReadAsAsync<UserResult>();
    }

    public async Task<IResult> HandleAsync(CreateCommand command)
    {
        var response = await this.httpClient
            .PostAsJsonAsync("User", command);

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
            .PutAsJsonAsync("User", command);

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
            .PutAsJsonAsync("User/Activate", command);

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
            .PutAsJsonAsync("User/Deactivate", command);

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
            .DeleteAsJsonAsync("User", command);

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

    // TODO: change
    public async Task<IResult> HandleAsync(LoginCommand command)
    {
        var errors = new List<KeyValuePair<string, string>>();

        if (string.IsNullOrWhiteSpace(command.Email))
        {
            errors.Add(new KeyValuePair<string, string>(nameof(command.Email), $"{nameof(command.Email)} is required."));
        }

        if (string.IsNullOrWhiteSpace(command.Password))
        {
            errors.Add(new KeyValuePair<string, string>(nameof(command.Password), $"{nameof(command.Password)} is required."));
        }

        if (errors.Any())
        {
            return Result.Invalid()
                .WithErrors(errors)
                .Build();
        }

        await this.accountService
            .SignInAsync(Guid.NewGuid(), command.Email, Role.Administrator, command.RememberMe);

        return Result.Success();
    }

    // TODO: change
    public async Task<IResult> HandleAsync(LogoutCommand command)
    {
        await this.accountService
            .SignOutAsync();

        return Result.Success();
    }
}
