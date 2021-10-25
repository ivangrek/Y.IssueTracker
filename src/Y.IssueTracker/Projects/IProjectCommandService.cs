namespace Y.IssueTracker.Projects
{
    using System.Threading.Tasks;
    using Commands;

    public interface IProjectCommandService
    {
        Task<IResult> ExecuteAsync(ICreateCommand command);

        Task<IResult> ExecuteAsync(IUpdateCommand command);

        Task<IResult> ExecuteAsync(IDeleteCommand command);

        Task<IResult> ExecuteAsync(IDeactivateCommand command);

        Task<IResult> ExecuteAsync(IActivateCommand command);
    }
}
