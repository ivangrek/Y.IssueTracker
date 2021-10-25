namespace Y.IssueTracker.Web.Models.User
{
    using System;
    using Users.Commands;
    using Users.Domain;

    public sealed class UpdateUserViewModel : IUpdateCommand
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        
        public Role Role { get; set; }
    }
}
