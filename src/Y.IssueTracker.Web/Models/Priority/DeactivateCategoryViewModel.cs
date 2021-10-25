namespace Y.IssueTracker.Web.Models.Priority
{
    using System;
    using Priorities.Commands;

    public sealed class DeactivateCategoryViewModel : IDeactivateCommand
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
