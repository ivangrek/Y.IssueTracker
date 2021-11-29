namespace Y.IssueTracker.Issues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Commands;
    using Domain;

    internal sealed class IssueCommandService : IIssueCommandService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IIssueRepository issueRepository;

        public IssueCommandService(
            IUnitOfWork unitOfWork,
            IIssueRepository issueRepository)
        {
            this.unitOfWork = unitOfWork;
            this.issueRepository = issueRepository;
        }

        public async Task<IResult> ExecuteAsync(ICreateCommand command)
        {
            var errors = new List<KeyValuePair<string, string>>();

            if (string.IsNullOrWhiteSpace(command.Name))
            {
                errors.Add(new KeyValuePair<string, string>(nameof(command.Name), $"{nameof(command.Name)} is required."));
            }

            if (string.IsNullOrWhiteSpace(command.Description))
            {
                errors.Add(new KeyValuePair<string, string>(nameof(command.Description), $"{nameof(command.Description)} is required."));
            }

            if (command.ProjectId == Guid.Empty)
            {
                errors.Add(new KeyValuePair<string, string>(nameof(command.ProjectId), "Project is required."));
            }

            if (command.CategoryId == Guid.Empty)
            {
                errors.Add(new KeyValuePair<string, string>(nameof(command.CategoryId), "Category is required."));
            }

            if (command.PriorityId == Guid.Empty)
            {
                errors.Add(new KeyValuePair<string, string>(nameof(command.PriorityId), "Priority is required."));
            }

            if (errors.Any())
            {
                return Result.Invalid()
                    .WithErrors(errors)
                    .Build();
            }

            var issue = new Issue(Guid.NewGuid())
            {
                Name = command.Name,
                Description = command.Description,
                ProjectId = command.ProjectId,
                CategoryId = command.CategoryId,
                PriorityId = command.PriorityId,
                Status = IssueStatus.Opened,
                AssignedUserId = command.AssignedUserId,
                AuthorUserId = command.AuthorUserId,
                CreatedOn = DateTime.Now
            };

            await this.issueRepository
                .AddAsync(issue);

            await this.unitOfWork
                .CommitAsync();

            return Result.Success();
        }

        public async Task<IResult> ExecuteAsync(IUpdateCommand command)
        {
            var errors = new List<KeyValuePair<string, string>>();

            if (string.IsNullOrWhiteSpace(command.Name))
            {
                errors.Add(new KeyValuePair<string, string>(nameof(command.Name), $"{nameof(command.Name)} is required."));
            }

            if (string.IsNullOrWhiteSpace(command.Description))
            {
                errors.Add(new KeyValuePair<string, string>(nameof(command.Description), $"{nameof(command.Description)} is required."));
            }

            if (command.ProjectId == Guid.Empty)
            {
                errors.Add(new KeyValuePair<string, string>(nameof(command.ProjectId), "Project is required."));
            }

            if (command.CategoryId == Guid.Empty)
            {
                errors.Add(new KeyValuePair<string, string>(nameof(command.CategoryId), "Category is required."));
            }

            if (command.PriorityId == Guid.Empty)
            {
                errors.Add(new KeyValuePair<string, string>(nameof(command.PriorityId), "Priority is required."));
            }

            if (errors.Any())
            {
                return Result.Invalid()
                    .WithErrors(errors)
                    .Build();
            }

            var issue = await this.issueRepository
                .QueryByIdAsync(command.Id);

            if (issue is null)
            {
                return Result.Failure()
                    .WithError("Not exist.")
                    .Build();
            }

            issue.Name = command.Name;
            issue.Description = command.Description;
            issue.ProjectId = command.ProjectId;
            issue.CategoryId = command.CategoryId;
            issue.PriorityId = command.PriorityId;
            issue.Status = command.Status;
            issue.AssignedUserId = command.AssignedUserId;

            await this.unitOfWork
                .CommitAsync();

            return Result.Success();
        }

        public async Task<IResult> ExecuteAsync(IDeleteCommand command)
        {
            var issue = await this.issueRepository
                .QueryByIdAsync(command.Id);

            if (issue is null)
            {
                return Result.Failure()
                    .WithError("Not exist.")
                    .Build();
            }

            this.issueRepository
                .Remove(issue);

            await this.unitOfWork
                .CommitAsync();

            return Result.Success();
        }
    }
}
