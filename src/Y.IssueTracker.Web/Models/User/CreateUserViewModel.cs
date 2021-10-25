namespace Y.IssueTracker.Web.Models.User
{
    using Users.Commands;
    using Users.Domain;

    public sealed class CreateUserViewModel : ICreateCommand
    {
        public string Name { get; set; }

        public Role Role { get; set; }
    }
}
