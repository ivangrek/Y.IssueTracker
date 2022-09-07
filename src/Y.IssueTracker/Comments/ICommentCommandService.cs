namespace Y.IssueTracker.Comments;

using System.Threading.Tasks;
using Commands;

public interface ICommentCommandService
{
    Task<IResult> HandleAsync(CreateCommand command);
}
