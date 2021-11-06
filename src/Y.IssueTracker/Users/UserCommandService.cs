namespace Y.IssueTracker.Users
{
    using System;
    using System.Threading.Tasks;
    using Commands;
    using Domain;

    internal sealed class UserCommandService : IUserCommandService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserRepository userRepository;

        public UserCommandService(
            IUnitOfWork unitOfWork,
            IUserRepository userRepository)
        {
            this.unitOfWork = unitOfWork;
            this.userRepository = userRepository;
        }

        public async Task<IResult> ExecuteAsync(ICreateCommand command)
        {
            if (string.IsNullOrWhiteSpace(command.Name))
            {
                return Result.Invalid()
                    .WithError(nameof(command.Name), $"{nameof(command.Name)} is required.")
                    .Build();
            }

            var user = new User(Guid.NewGuid())
            {
                Name = command.Name,
                Role = command.Role,
                IsActive = true,
                IsDefault = false
            };

            this.userRepository
                .Add(user);

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

            var user = await this.userRepository
                .QueryByIdAsync(command.Id);

            if (user is null)
            {
                return Result.Failure()
                    .WithError(string.Empty, "Not exist.")
                    .Build();
            }

            if (!user.IsActive)
            {
                return Result.Failure()
                    .WithError(string.Empty, "Invalid operation.")
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
                    .WithError(string.Empty, "Not exist.")
                    .Build();
            }

            if (user.IsDefault)
            {
                return Result.Failure()
                    .WithError(string.Empty, "Invalid operation.")
                    .Build();
            }

            this.userRepository
                .Remove(user);

            await this.unitOfWork
                .CommitAsync();

            return Result.Success();
        }

        public async Task<IResult> ExecuteAsync(IDeactivateCommand command)
        {
            var user = await this.userRepository
                .QueryByIdAsync(command.Id);

            if (user is null)
            {
                return Result.Failure()
                    .WithError(string.Empty, "Not exist.")
                    .Build();
            }

            if (!user.IsActive || user.IsDefault)
            {
                return Result.Failure()
                    .WithError(string.Empty, "Invalid operation.")
                    .Build();
            }

            user.IsActive = false;

            await this.unitOfWork
                .CommitAsync();

            return Result.Success();
        }

        public async Task<IResult> ExecuteAsync(IActivateCommand command)
        {
            var user = await this.userRepository
                .QueryByIdAsync(command.Id);

            if (user is null)
            {
                return Result.Failure()
                    .WithError(string.Empty, "Not exist.")
                    .Build();
            }

            if (user.IsActive || user.IsDefault)
            {
                return Result.Failure()
                    .WithError(string.Empty, "Invalid operation.")
                    .Build();
            }

            user.IsActive = true;

            await this.unitOfWork
                .CommitAsync();

            return Result.Success();
        }
    }
}
