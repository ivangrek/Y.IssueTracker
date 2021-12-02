namespace Y.IssueTracker.Priorities;

using System;
using System.Threading.Tasks;
using Commands;
using Domain;

internal sealed class PriorityCommandService : IPriorityCommandService
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IPriorityRepository priorityRepository;

    public PriorityCommandService(
        IUnitOfWork unitOfWork,
        IPriorityRepository priorityRepository)
    {
        this.unitOfWork = unitOfWork;
        this.priorityRepository = priorityRepository;
    }

    public async Task<IResult> ExecuteAsync(ICreateCommand command)
    {
        if (string.IsNullOrWhiteSpace(command.Name))
        {
            return Result.Invalid()
               .WithError(nameof(command.Name), $"{nameof(command.Name)} is required.")
               .Build();
        }

        var priority = new Priority(Guid.NewGuid())
        {
            Name = command.Name,
            Weight = command.Weight,
            IsActive = true
        };

        await this.priorityRepository
            .AddAsync(priority);

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

        var priority = await this.priorityRepository
            .FindByIdAsync(command.Id);

        if (priority is null)
        {
            return Result.Failure()
                .WithError("Not exist.")
                .Build();
        }

        if (!priority.IsActive)
        {
            return Result.Failure()
                .WithError("Invalid operation.")
                .Build();
        }

        priority.Name = command.Name;
        priority.Weight = command.Weight;

        await this.unitOfWork
            .CommitAsync();

        return Result.Success();
    }

    public async Task<IResult> ExecuteAsync(IDeleteCommand command)
    {
        var priority = await this.priorityRepository
            .FindByIdAsync(command.Id);

        if (priority is null)
        {
            return Result.Failure()
                .WithError("Not exist.")
                .Build();
        }

        this.priorityRepository
            .Remove(priority);

        await this.unitOfWork
            .CommitAsync();

        return Result.Success();
    }

    public async Task<IResult> ExecuteAsync(IDeactivateCommand command)
    {
        var priority = await this.priorityRepository
            .FindByIdAsync(command.Id);

        if (priority is null)
        {
            return Result.Failure()
                .WithError("Not exist.")
                .Build();
        }

        if (!priority.IsActive)
        {
            return Result.Failure()
                .WithError("Invalid operation.")
                .Build();
        }

        priority.IsActive = false;

        await this.unitOfWork
            .CommitAsync();

        return Result.Success();
    }

    public async Task<IResult> ExecuteAsync(IActivateCommand command)
    {
        var priority = await this.priorityRepository
            .FindByIdAsync(command.Id);

        if (priority is null)
        {
            return Result.Failure()
                .WithError("Not exist.")
                .Build();
        }

        if (priority.IsActive)
        {
            return Result.Failure()
                .WithError("Invalid operation.")
                .Build();
        }

        priority.IsActive = true;

        await this.unitOfWork
            .CommitAsync();

        return Result.Success();
    }
}
