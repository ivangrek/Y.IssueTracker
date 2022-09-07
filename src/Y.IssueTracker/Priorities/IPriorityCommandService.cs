namespace Y.IssueTracker.Priorities;

using System.Threading.Tasks;
using Commands;

public interface IPriorityCommandService
{
    Task<IResult> HandleAsync(CreateCommand command);

    Task<IResult> HandleAsync(UpdateCommand command);

    Task<IResult> HandleAsync(DeleteCommand command);

    Task<IResult> HandleAsync(ActivateCommand command);

    Task<IResult> HandleAsync(DeactivateCommand command);
}
