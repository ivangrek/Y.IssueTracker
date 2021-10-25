namespace Y.IssueTracker.Web.Models.Issue
{
    using System;
    using Issues.Commands;

    public sealed class DeleteIssueViewModel : IDeleteCommand
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
