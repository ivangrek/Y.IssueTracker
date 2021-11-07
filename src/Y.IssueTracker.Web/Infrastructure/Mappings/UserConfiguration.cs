namespace Y.IssueTracker.Web.Infrastructure.Mappings
{
    using Microsoft.EntityFrameworkCore;
    using Users.Domain;

    internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<User> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Name)
                .IsRequired();

            builder.HasIndex(x => x.Name)
                .IsUnique();

            builder
                .Property(x => x.Role)
                .IsRequired();

            builder
                .Property(x => x.IsActive)
                .IsRequired();

            builder
                .Property(x => x.IsDefault)
                .IsRequired();
        }
    }
}
