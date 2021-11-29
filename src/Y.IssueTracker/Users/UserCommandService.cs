namespace Y.IssueTracker.Users;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Commands;
using Domain;

internal sealed class UserCommandService : IUserCommandService
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IUserRepository userRepository;
    private readonly IAccountService accountService;
    private readonly IUserQueryService userQueryService;
    private readonly IPasswordHasher passwordHasher;

    public UserCommandService(
        IUnitOfWork unitOfWork,
        IUserRepository userRepository,
        IAccountService accountService,
        IUserQueryService userQueryService,
        IPasswordHasher passwordHasher)
    {
        this.unitOfWork = unitOfWork;
        this.userRepository = userRepository;
        this.accountService = accountService;
        this.userQueryService = userQueryService;
        this.passwordHasher = passwordHasher;
    }

    public async Task<IResult> ExecuteAsync(ICreateCommand command)
    {
        if (string.IsNullOrWhiteSpace(command.Name))
        {
            return Result.Invalid()
                .WithError(nameof(command.Name), $"{nameof(command.Name)} is required.")
                .Build();
        }

        var userExists = this.userRepository
            .QueryAll()
            .Any(x => x.Name == command.Name);

        if (userExists)
        {
            return Result.Invalid()
                .WithError(nameof(command.Name), $"User with '{command.Name}' exists.")
                .Build();
        }

        var user = new User(Guid.NewGuid())
        {
            Name = command.Name,
            Role = command.Role,
            IsActive = true,
            IsDefault = false
        };

        await this.userRepository
            .AddAsync(user);

        await this.unitOfWork
            .CommitAsync();

        return Result.Success();
    }

    public async Task<IResult> ExecuteAsync(IUpdateCommand command)
    {
        if (string.IsNullOrWhiteSpace(command.Name))
        {
            return Result.Invalid()
                .WithError(nameof(command.Name), $"{nameof(command.Name)} is required.")
                .Build();
        }

        var userExists = this.userRepository
            .QueryAll()
            .Any(x => x.Name == command.Name);

        if (userExists)
        {
            return Result.Invalid()
                .WithError(nameof(command.Name), $"User with '{command.Name}' exists.")
                .Build();
        }

        var user = await this.userRepository
            .QueryByIdAsync(command.Id);

        if (user is null)
        {
            return Result.Failure()
                .WithError("Not exist.")
                .Build();
        }

        if (!user.IsActive)
        {
            return Result.Failure()
                .WithError("Invalid operation.")
                .Build();
        }

        user.Name = command.Name;
        user.Role = command.Role;

        await this.unitOfWork
            .CommitAsync();

        return Result.Success();
    }

    public async Task<IResult> ExecuteAsync(IDeleteCommand command)
    {
        var user = await this.userRepository
            .QueryByIdAsync(command.Id);

        if (user is null)
        {
            return Result.Failure()
                .WithError("Not exist.")
                .Build();
        }

        if (user.IsDefault)
        {
            return Result.Failure()
                .WithError("Invalid operation.")
                .Build();
        }

        this.userRepository
            .Remove(user);

        var commitTask = this.unitOfWork
            .CommitAsync();

        var signOutTask = this.accountService
            .SignOutAsync(user.Id);

        await commitTask;
        await signOutTask;

        return Result.Success();
    }

    public async Task<IResult> ExecuteAsync(IDeactivateCommand command)
    {
        var user = await this.userRepository
            .QueryByIdAsync(command.Id);

        if (user is null)
        {
            return Result.Failure()
                .WithError("Not exist.")
                .Build();
        }

        if (!user.IsActive || user.IsDefault)
        {
            return Result.Failure()
                .WithError("Invalid operation.")
                .Build();
        }

        user.IsActive = false;

        var commitTask = this.unitOfWork
            .CommitAsync();

        var signOutTask = this.accountService
            .SignOutAsync(user.Id);

        await commitTask;
        await signOutTask;

        return Result.Success();
    }

    public async Task<IResult> ExecuteAsync(IActivateCommand command)
    {
        var user = await this.userRepository
            .QueryByIdAsync(command.Id);

        if (user is null)
        {
            return Result.Failure()
                .WithError("Not exist.")
                .Build();
        }

        if (user.IsActive || user.IsDefault)
        {
            return Result.Failure()
                .WithError("Invalid operation.")
                .Build();
        }

        user.IsActive = true;

        await this.unitOfWork
            .CommitAsync();

        return Result.Success();
    }

    public async Task<IResult> ExecuteAsync(ILoginCommand command)
    {
        var errors = new List<KeyValuePair<string, string>>();

        if (string.IsNullOrWhiteSpace(command.Email))
        {
            errors.Add(new KeyValuePair<string, string>(nameof(command.Email), $"{nameof(command.Email)} is required."));
        }

        if (string.IsNullOrWhiteSpace(command.Password))
        {
            errors.Add(new KeyValuePair<string, string>(nameof(command.Password), $"{nameof(command.Password)} is required."));
        }

        if (errors.Any())
        {
            return Result.Invalid()
                .WithErrors(errors)
                .Build();
        }

        var passwordHash = this.passwordHasher
            .HashPassword(command.Password);

        var user = await this.userQueryService
            .QueryByCredentialsAsync(command.Email, passwordHash);

        if (user is null)
        {
            return Result.Invalid()
                .WithError("Invalid email or password.")
                .Build();
        }

        await this.accountService
            .SignInAsync(user.Id, user.Name, user.Role, command.RememberMe);

        return Result.Success();
    }
}
