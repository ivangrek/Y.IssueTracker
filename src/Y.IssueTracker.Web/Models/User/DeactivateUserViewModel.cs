namespace Y.IssueTracker.Web.Models.User;

using System;
using Users.Commands;

public sealed class DeactivateUserViewModel : IDeactivateCommand
{
    public Guid Id { get; set; }

    public string Name { get; set; }
}
