﻿namespace Y.IssueTracker.Users;

using System.Threading.Tasks;
using Commands;

public interface IUserCommandService
{
    Task<IResult> ExecuteAsync(ICreateCommand command);

    Task<IResult> ExecuteAsync(IUpdateCommand command);

    Task<IResult> ExecuteAsync(IDeleteCommand command);

    Task<IResult> ExecuteAsync(IDeactivateCommand command);

    Task<IResult> ExecuteAsync(IActivateCommand command);

    Task<IResult> ExecuteAsync(IRegisterCommand command);

    Task<IResult> ExecuteAsync(ILoginCommand command);

    Task<IResult> ExecuteAsync(ILogoutCommand command);

    Task<IResult> ExecuteAsync(IResetPasswordCommand command);
}
