namespace Y.IssueTracker.Web.Infrastructure.Mappings
{
    using Categories.Domain;
    using Microsoft.EntityFrameworkCore;

    internal sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Category> builder)
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
