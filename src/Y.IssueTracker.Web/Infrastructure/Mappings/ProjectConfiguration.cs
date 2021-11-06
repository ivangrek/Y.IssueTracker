namespace Y.IssueTracker.Web.Infrastructure.Mappings
{
    using Microsoft.EntityFrameworkCore;
    using Projects.Domain;

    internal sealed class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Project> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Name)
                .IsRequired();

            builder
                .Property(x => x.IsActive)
                .IsRequired();
        }
    }
}
