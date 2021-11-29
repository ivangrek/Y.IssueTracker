namespace Y.IssueTracker.Web.Infrastructure.Mappings;

using Microsoft.EntityFrameworkCore;
using Priorities.Domain;

internal sealed class PriorityConfiguration : IEntityTypeConfiguration<Priority>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Priority> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Name)
            .IsRequired();

        builder
            .Property(x => x.Weight)
            .IsRequired();

        builder
            .Property(x => x.IsActive)
            .IsRequired();
    }
}
