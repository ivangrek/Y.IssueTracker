namespace Y.IssueTracker.Comments
{
    using System;
    using System.Threading.Tasks;
    using Commands;
    using Domain;

    internal sealed class CommentCommandService : ICommentCommandService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ICommentRepository commentRepository;

        public CommentCommandService(
            IUnitOfWork unitOfWork,
            ICommentRepository commentRepository)
        {
            this.unitOfWork = unitOfWork;
            this.commentRepository = commentRepository;
        }

        public async Task<IResult> ExecuteAsync(ICreateCommand command)
        {
            if (string.IsNullOrWhiteSpace(command.Text))
            {
                return Result.Invalid()
                    .WithError(nameof(command.Text), $"{nameof(command.Text)} is required.")
                    .Build();
            }

            var comment = new Comment(Guid.NewGuid())
            {
                IssueId = command.IssueId,
                Text = command.Text,
                AuthorUserId = command.AuthorUserId,
                CreatedOn = DateTime.Now
            };

            this.commentRepository
                .Add(comment);

            await this.unitOfWork
                .CommitAsync();

            return Result.Success();
        }
    }
}
