namespace Y.IssueTracker.Projects.Results;

public interface IProjectResult
{
    Guid Id { get; }

    string Name { get; }

    bool IsActive { get; }
}
