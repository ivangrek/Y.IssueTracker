namespace Y.IssueTracker.Issues;

using System.Threading.Tasks;
using Commands;

public interface IIssueCommandService
{
    Task<IResult> HandleAsync(CreateCommand command);

    Task<IResult> HandleAsync(UpdateCommand command);

    Task<IResult> HandleAsync(DeleteCommand command);
}
