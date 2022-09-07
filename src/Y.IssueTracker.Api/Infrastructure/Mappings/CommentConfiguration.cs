namespace Y.IssueTracker.Api.Infrastructure.Mappings;

using Comments.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal sealed class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.IssueId)
            .IsRequired();

        builder
            .Property(x => x.Text)
            .IsRequired();

        builder
            .Property(x => x.AuthorUserId)
            .IsRequired();

        builder
            .Property(x => x.CreatedOn)
            .IsRequired();
    }
}
