namespace Y.IssueTracker.Web.Infrastructure.Mappings
{
    using Issues.Domain;
    using Microsoft.EntityFrameworkCore;

    internal sealed class IssueConfiguration : IEntityTypeConfiguration<Issue>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Issue> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Name)
                .IsRequired();

            builder
                .Property(x => x.Description)
                .IsRequired();

            builder
                .Property(x => x.ProjectId)
                .IsRequired();

            builder
                .Property(x => x.CategoryId)
                .IsRequired();

            builder
                .Property(x => x.PriorityId)
                .IsRequired();

            builder
                .Property(x => x.Status)
                .IsRequired();

            builder
                .Property(x => x.AssignedUserId);

            builder
                .Property(x => x.AuthorUserId)
                .IsRequired();

            builder
                .Property(x => x.CreatedOn)
                .IsRequired();
        }
    }
}
