namespace Y.IssueTracker.Projects;

using System.Threading.Tasks;
using Commands;

public interface IProjectCommandService
{
    Task<IResult> HandleAsync(CreateCommand command);

    Task<IResult> HandleAsync(UpdateCommand command);

    Task<IResult> HandleAsync(DeleteCommand command);

    Task<IResult> HandleAsync(ActivateCommand command);

    Task<IResult> HandleAsync(DeactivateCommand command);
}
