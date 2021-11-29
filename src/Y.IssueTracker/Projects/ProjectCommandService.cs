namespace Y.IssueTracker.Projects;

using System;
using System.Threading.Tasks;
using Commands;
using Domain;

internal sealed class ProjectCommandService : IProjectCommandService
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IProjectRepository projectRepository;

    public ProjectCommandService(
        IUnitOfWork unitOfWork,
        IProjectRepository projectRepository)
    {
        this.unitOfWork = unitOfWork;
        this.projectRepository = projectRepository;
    }

    public async Task<IResult> ExecuteAsync(ICreateCommand command)
    {
        if (string.IsNullOrWhiteSpace(command.Name))
        {
            return Result.Invalid()
               .WithError(nameof(command.Name), $"{nameof(command.Name)} is required.")
               .Build();
        }

        var project = new Project(Guid.NewGuid())
        {
            Name = command.Name,
            IsActive = true
        };

        await this.projectRepository
            .AddAsync(project);

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

        var project = await this.projectRepository
            .QueryByIdAsync(command.Id);

        if (project is null)
        {
            return Result.Failure()
               .WithError("Not exist.")
               .Build();
        }

        if (!project.IsActive)
        {
            return Result.Failure()
               .WithError("Invalid operation.")
               .Build();
        }

        project.Name = command.Name;

        await this.unitOfWork
            .CommitAsync();

        return Result.Success();
    }

    public async Task<IResult> ExecuteAsync(IDeleteCommand command)
    {
        var project = await this.projectRepository
            .QueryByIdAsync(command.Id);

        if (project is null)
        {
            return Result.Failure()
                .WithError("Not exist.")
                .Build();
        }

        this.projectRepository
            .Remove(project);

        await this.unitOfWork
            .CommitAsync();

        return Result.Success();
    }

    public async Task<IResult> ExecuteAsync(IDeactivateCommand command)
    {
        var project = await this.projectRepository
            .QueryByIdAsync(command.Id);

        if (project is null)
        {
            return Result.Failure()
                .WithError("Not exist.")
                .Build();
        }

        if (!project.IsActive)
        {
            return Result.Failure()
                .WithError("Invalid operation.")
                .Build();
        }

        project.IsActive = false;

        await this.unitOfWork
            .CommitAsync();

        return Result.Success();
    }

    public async Task<IResult> ExecuteAsync(IActivateCommand command)
    {
        var project = await this.projectRepository
            .QueryByIdAsync(command.Id);

        if (project is null)
        {
            return Result.Failure()
                .WithError("Not exist.")
                .Build();
        }

        if (project.IsActive)
        {
            return Result.Failure()
                .WithError("Invalid operation.")
                .Build();
        }

        project.IsActive = true;

        await this.unitOfWork
            .CommitAsync();

        return Result.Success();
    }
}
