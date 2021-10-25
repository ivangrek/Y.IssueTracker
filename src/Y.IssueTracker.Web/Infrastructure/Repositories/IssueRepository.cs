﻿namespace Y.IssueTracker.Web.Infrastructure.Repositories
{
    using Issues.Domain;

    internal sealed class IssueRepository : Repository<Issue>, IIssueRepository
    {
        public IssueRepository(ApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        {
        }
    }
}
