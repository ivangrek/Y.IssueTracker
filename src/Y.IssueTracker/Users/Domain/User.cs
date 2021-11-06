namespace Y.IssueTracker.Users.Domain
{
    using System;

    public sealed class User : IEntity
    {
        public User(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }

        public string Name { get; set; }

        public Role Role { get; set; }

        public bool IsActive { get; set; }
    }
}
