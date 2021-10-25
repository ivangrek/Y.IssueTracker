namespace Y.IssueTracker.Users.Domain
{
    using System;

    public sealed class User : IEntity
    {
        public static Guid UserId = new Guid("DA22D613-6A1B-4DFA-8AD5-87514E98079A");
        public static Guid ManagerId = new Guid("B5D61694-B355-46DC-AFA1-2C385D2B3A7D");
        public static Guid AdministratorId = new Guid("635ECF0D-A569-4E94-9C14-29F5D3FCF220");

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
