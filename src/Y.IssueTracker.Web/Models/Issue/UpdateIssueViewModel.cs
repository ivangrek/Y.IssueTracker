namespace Y.IssueTracker.Web.Models.Issue
{
    using System;
    using System.ComponentModel;
    using Issues.Commands;
    using Issues.Domain;

    public sealed class UpdateIssueViewModel : IUpdateCommand
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [DisplayName("Project")]
        public Guid ProjectId { get; set; }

        [DisplayName("Category")]
        public Guid CategoryId { get; set; }

        [DisplayName("Priority")]
        public Guid PriorityId { get; set; }

        [DisplayName("Status")]
        public IssueStatus Status { get; set; }

        [DisplayName("Assigned")]
        public Guid AssignedUserId { get; set; }
    }
}
