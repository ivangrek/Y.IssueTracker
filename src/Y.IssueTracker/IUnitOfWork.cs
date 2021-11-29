namespace Y.IssueTracker;

using System.Threading.Tasks;

public interface IUnitOfWork
{
    Task CommitAsync();
}
