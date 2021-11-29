namespace Y.IssueTracker.Priorities;

using System.Threading.Tasks;
using Commands;

public interface IPriorityCommandService
{
    Task<IResult> ExecuteAsync(ICreateCommand command);

    Task<IResult> ExecuteAsync(IUpdateCommand command);

    Task<IResult> ExecuteAsync(IDeleteCommand command);

    Task<IResult> ExecuteAsync(IDeactivateCommand command);

    Task<IResult> ExecuteAsync(IActivateCommand command);
}
