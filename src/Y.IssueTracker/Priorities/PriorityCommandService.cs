namespace Y.IssueTracker.Priorities;

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

    public async Task<IResult> HandleAsync(CreateCommand command)
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

    public async Task<IResult> HandleAsync(UpdateCommand command)
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

    public async Task<IResult> HandleAsync(DeleteCommand command)
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

    public async Task<IResult> HandleAsync(DeactivateCommand command)
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

    public async Task<IResult> HandleAsync(ActivateCommand command)
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
