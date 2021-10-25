namespace Y.IssueTracker.Issues
{
    using System.Threading.Tasks;
    using Commands;

    public interface IIssueCommandService
    {
        Task<IResult> ExecuteAsync(ICreateCommand command);

        Task<IResult> ExecuteAsync(IUpdateCommand command);

        Task<IResult> ExecuteAsync(IDeleteCommand command);
    }
}
