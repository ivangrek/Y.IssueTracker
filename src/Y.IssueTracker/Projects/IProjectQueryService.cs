namespace Y.IssueTracker.Projects;

using Results;

public interface IProjectQueryService
{
    Task<IProjectResult[]> QueryAllAsync();

    Task<IProjectResult> QueryByIdAsync(Guid id);
}
