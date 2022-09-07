namespace Y.IssueTracker.Users;

using System.Threading.Tasks;
using Commands;

public interface IUserCommandService
{
    Task<IResult> HandleAsync(CreateCommand command);

    Task<IResult> HandleAsync(UpdateCommand command);

    Task<IResult> HandleAsync(DeactivateCommand command);

    Task<IResult> HandleAsync(ActivateCommand command);

    Task<IResult> HandleAsync(DeleteCommand command);

    //Task<IResult> HandleAsync(RegisterCommand command);

    //Task<IResult> HandleAsync(LoginCommand command);

    //Task<IResult> HandleAsync(LogoutCommand command);

    //Task<IResult> HandleAsync(ResetPasswordCommand command);
}
