namespace Y.IssueTracker.Users;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Commands;
using Domain;

internal sealed class UserCommandService : IUserCommandService
{
    private const string EmailPattern = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

    private readonly IUnitOfWork unitOfWork;
    private readonly IUserRepository userRepository;
    private readonly IAccountService accountService;
    private readonly IUserQueryService userQueryService;
    private readonly IPasswordHasher passwordHasher;
    private readonly IEmailService emailService;

    public UserCommandService(
        IUnitOfWork unitOfWork,
        IUserRepository userRepository,
        IAccountService accountService,
        IUserQueryService userQueryService,
        IPasswordHasher passwordHasher,
        IEmailService emailService)
    {
        this.unitOfWork = unitOfWork;
        this.userRepository = userRepository;
        this.accountService = accountService;
        this.userQueryService = userQueryService;
        this.passwordHasher = passwordHasher;
        this.emailService = emailService;
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
            .FindByIdAsync(command.Id);

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
            .FindByIdAsync(command.Id);

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
            .FindByIdAsync(command.Id);

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
            .FindByIdAsync(command.Id);

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

    public async Task<IResult> ExecuteAsync(IRegisterCommand command)
    {
        var errors = new List<KeyValuePair<string, string>>();

        if (string.IsNullOrWhiteSpace(command.Email))
        {
            errors.Add(new KeyValuePair<string, string>(nameof(command.Email), $"{nameof(command.Email)} is required."));
        }
        else if (!Regex.IsMatch(command.Email, EmailPattern))
        {
            errors.Add(new KeyValuePair<string, string>(nameof(command.Email), "Invalid email format."));
        }
        else
        {
            var userExists = await this.userQueryService
                .QueryCheckUserExistsAsync(command.Email);

            if (userExists)
            {
                errors.Add(new KeyValuePair<string, string>(nameof(command.Email), $"{nameof(command.Email)} exists."));
            }
        }

        if (string.IsNullOrWhiteSpace(command.Password))
        {
            errors.Add(new KeyValuePair<string, string>(nameof(command.Password), $"{nameof(command.Password)} is required."));
        }
        else if (command.Password != command.PasswordConfirm)
        {
            errors.Add(new KeyValuePair<string, string>(nameof(command.PasswordConfirm), $"Should be the same with {nameof(command.Password)}."));
        }

        if (errors.Any())
        {
            return Result.Invalid()
                .WithErrors(errors)
                .Build();
        }

        var passwordHash = this.passwordHasher
            .HashPassword(command.Password);

        var user = new User(Guid.NewGuid())
        {
            Name = "No name",
            Email = command.Email,
            Password = passwordHash,
            IsActive = true,
            IsDefault = false,
            Role = Role.User
        };

        await this.userRepository
            .AddAsync(user);

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

    public async Task<IResult> ExecuteAsync(ILogoutCommand command)
    {
        await this.accountService
            .SignOutAsync();

        return Result.Success();
    }

    public async Task<IResult> ExecuteAsync(IResetPasswordCommand command)
    {
        var errors = new List<KeyValuePair<string, string>>();

        if (string.IsNullOrWhiteSpace(command.Email))
        {
            errors.Add(new KeyValuePair<string, string>(nameof(command.Email), $"{nameof(command.Email)} is required."));
        }
        else if (!Regex.IsMatch(command.Email, EmailPattern))
        {
            errors.Add(new KeyValuePair<string, string>(nameof(command.Email), "Invalid email format."));
        }
        else
        {
            var userExists = await this.userQueryService
                .QueryCheckUserExistsAsync(command.Email);

            if (!userExists)
            {
                errors.Add(new KeyValuePair<string, string>(nameof(command.Email), $"{nameof(command.Email)} not exists."));
            }
        }

        if (errors.Any())
        {
            return Result.Invalid()
                .WithErrors(errors)
                .Build();
        }

        var password = Guid.NewGuid().ToString();
        var user = await this.userRepository
            .FindByEmailAsync(command.Email);

        user.Password = this.passwordHasher
            .HashPassword(password);

        await this.unitOfWork
            .CommitAsync();

        await this.emailService
            .SendNewPasswordAsync(user.Email, password);

        return Result.Success();
    }
}
