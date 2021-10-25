namespace Y.IssueTracker.Web.Models.Project
{
    using System;
    using Projects.Commands;

    public sealed class DeleteProjectViewModel : IDeleteCommand
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
