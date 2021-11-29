namespace Y.IssueTracker.Web.Infrastructure;

using System;
using System.Text;
using Y.IssueTracker.Users;

internal sealed class PasswordHasher : IPasswordHasher
{
    public string HashPassword(string password)
    {
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(password));
    }
}
