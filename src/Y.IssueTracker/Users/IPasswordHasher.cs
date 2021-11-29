namespace Y.IssueTracker.Users;

public interface IPasswordHasher
{
    string HashPassword(string password);
}
