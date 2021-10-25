namespace Y.IssueTracker.Users.Commands
{
    using Domain;

    public interface ICreateCommand
    {
        string Name { get; }

        Role Role { get; }
    }
}
