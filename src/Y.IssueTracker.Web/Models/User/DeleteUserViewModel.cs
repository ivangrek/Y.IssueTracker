﻿namespace Y.IssueTracker.Web.Models.User;

using System;
using Users.Commands;

public sealed class DeleteUserViewModel : IDeleteCommand
{
    public Guid Id { get; set; }

    public string Name { get; set; }
}
