namespace Y.IssueTracker.Projects;

using Results;
using Y.IssueTracker.Projects.Queries;

public interface IProjectQueryService
{
    Task<ProjectResult[]> HandleAsync(GetAllQuery query);

    Task<ProjectResult> HandleAsync(GetByIdQuery query);
}
