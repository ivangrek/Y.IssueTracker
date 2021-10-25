﻿namespace Y.IssueTracker.Projects.Domain
{
    using System;

    public sealed class Project : IEntity
    {
        public Project(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }

        public string Name { get; set; }

        public bool IsActive { get; set; }
    }
}
