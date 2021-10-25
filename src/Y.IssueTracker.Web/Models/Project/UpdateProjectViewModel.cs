﻿namespace Y.IssueTracker.Web.Models.Project
{
    using System;
    using Projects.Commands;

    public sealed class UpdateProjectViewModel : IUpdateCommand
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
