namespace Y.IssueTracker.Web.Models.User
{
    using System;
    using Users.Commands;

    public sealed class ActivateUserViewModel : IActivateCommand
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
