namespace Y.IssueTracker.Web.Infrastructure.QueryServices.Results
{
    using System;
    using Comments.Results;

    internal sealed class CommentForViewResult : ICommentForViewResult
    {
        public Guid Id { get; init; }

        public string Text { get; init; }

        public Guid AuthorUserId { get; init; }

        public string AuthorUserName { get; init; }

        public DateTime CreatedOn { get; init; }
    }
}
