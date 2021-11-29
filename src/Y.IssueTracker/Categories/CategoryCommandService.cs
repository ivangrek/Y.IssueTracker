namespace Y.IssueTracker.Categories;

using System;
using System.Threading.Tasks;
using Commands;
using Domain;

internal sealed class CategoryCommandService : ICategoryCommandService
{
    private readonly IUnitOfWork unitOfWork;
    private readonly ICategoryRepository categoryRepository;

    public CategoryCommandService(
        IUnitOfWork unitOfWork,
        ICategoryRepository categoryRepository)
    {
        this.unitOfWork = unitOfWork;
        this.categoryRepository = categoryRepository;
    }

    public async Task<IResult> ExecuteAsync(ICreateCommand command)
    {
        if (string.IsNullOrWhiteSpace(command.Name))
        {
            return Result.Invalid()
                .WithError(nameof(command.Name), "Name is required.")
                .Build();
        }

        var category = new Category(Guid.NewGuid())
        {
            Name = command.Name,
            IsActive = true
        };

        await this.categoryRepository
            .AddAsync(category);

        await this.unitOfWork
            .CommitAsync();

        return Result.Success();
    }

    public async Task<IResult> ExecuteAsync(IUpdateCommand command)
    {
        if (string.IsNullOrWhiteSpace(command.Name))
        {
            return Result.Invalid()
                .WithError(nameof(command.Name), "Name is required.")
                .Build();
        }

        var category = await this.categoryRepository
            .QueryByIdAsync(command.Id);

        if (category is null)
        {
            return Result.Failure()
                .WithError("Not exist.")
                .Build();
        }

        if (!category.IsActive)
        {
            return Result.Failure()
                .WithError("Invalid operation.")
                .Build();
        }

        category.Name = command.Name;

        await this.unitOfWork
            .CommitAsync();

        return Result.Success();
    }

    public async Task<IResult> ExecuteAsync(IDeleteCommand command)
    {
        var category = await this.categoryRepository
            .QueryByIdAsync(command.Id);

        if (category is null)
        {
            return Result.Failure()
                .WithError("Not exist.")
                .Build();
        }

        this.categoryRepository
            .Remove(category);

        await this.unitOfWork
            .CommitAsync();

        return Result.Success();
    }

    public async Task<IResult> ExecuteAsync(IDeactivateCommand command)
    {
        var category = await this.categoryRepository
            .QueryByIdAsync(command.Id);

        if (category is null)
        {
            return Result.Failure()
                .WithError("Not exist.")
                .Build();
        }

        if (!category.IsActive)
        {
            return Result.Failure()
                .WithError("Invalid operation.")
                .Build();
        }

        category.IsActive = false;

        await this.unitOfWork
            .CommitAsync();

        return Result.Success();
    }

    public async Task<IResult> ExecuteAsync(IActivateCommand command)
    {
        var category = await this.categoryRepository
            .QueryByIdAsync(command.Id);

        if (category is null)
        {
            return Result.Failure()
                .WithError("Not exist.")
                .Build();
        }

        if (category.IsActive)
        {
            return Result.Failure()
                .WithError("Invalid operation.")
                .Build();
        }

        category.IsActive = true;

        await this.unitOfWork
            .CommitAsync();

        return Result.Success();
    }
}
