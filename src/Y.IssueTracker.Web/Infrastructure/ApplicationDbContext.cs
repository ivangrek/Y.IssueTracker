namespace Y.IssueTracker.Web.Infrastructure
{
    using Categories.Domain;
    using Comments.Domain;
    using Issues.Domain;
    using Microsoft.EntityFrameworkCore;
    using Priorities.Domain;
    using Projects.Domain;
    using Users.Domain;

    public sealed class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Priority> Priorities { get; set; }

        public DbSet<Issue> Issues { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Category>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Priority>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Issue>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Issue>()
                .Property(x => x.AuthorUserId);

            modelBuilder.Entity<Issue>()
                .Property(x => x.CreatedOn);

            modelBuilder.Entity<Comment>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Comment>()
                .Property(x => x.AuthorUserId);

            modelBuilder.Entity<Comment>()
                .Property(x => x.CreatedOn);

            modelBuilder.Entity<User>()
                .HasKey(x => x.Id);
        }
    }
}
