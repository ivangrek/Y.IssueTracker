namespace Y.IssueTracker.Priorities.Commands
{
    public interface ICreateCommand
    {
        string Name { get; }

        int Weight { get; }
    }
}
