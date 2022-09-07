namespace Y.IssueTracker.Web.Services;

using System.Net;
using Y.IssueTracker.Comments.Commands;
using Y.IssueTracker.Comments.Queries;
using Y.IssueTracker.Comments.Results;

public interface ICommentService
{
    Task<CommentForViewResult[]> HandleAsync(GetCommentsForViewQuery query);

    Task<IResult> HandleAsync(CreateCommand command);
}

internal sealed class CommentService : ICommentService
{
    private readonly HttpClient httpClient;

    public CommentService(IHttpClientFactory httpClientFactory)
    {
        this.httpClient = httpClientFactory.CreateClient("api");
    }

    public async Task<CommentForViewResult[]> HandleAsync(GetCommentsForViewQuery query)
    {
        var response = await this.httpClient
            .GetAsync($"Comment/{query.IssueId}");

        response.EnsureSuccessStatusCode();

        return await response.Content
            .ReadAsAsync<CommentForViewResult[]>();
    }

    public async Task<IResult> HandleAsync(CreateCommand command)
    {
        var response = await this.httpClient
            .PostAsJsonAsync("Comment", command);

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
