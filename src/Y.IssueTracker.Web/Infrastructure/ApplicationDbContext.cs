namespace Y.IssueTracker.Web.Infrastructure;

using System;
using Categories.Domain;
using Comments.Domain;
using Issues.Domain;
using Mappings;
using Microsoft.EntityFrameworkCore;
using Priorities.Domain;
using Projects.Domain;
using Users.Domain;
using Y.IssueTracker.Users;

public sealed class ApplicationDbContext : DbContext
{
    private readonly IPasswordHasher passwordHasher;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IPasswordHasher passwordHasher)
        : base(options)
    {
        this.passwordHasher = passwordHasher;
    }

    public DbSet<User> Users { get; set; }

    public DbSet<Project> Projects { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Priority> Priorities { get; set; }

    public DbSet<Issue> Issues { get; set; }

    public DbSet<Comment> Comments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new PriorityConfiguration());
        modelBuilder.ApplyConfiguration(new IssueConfiguration());
        modelBuilder.ApplyConfiguration(new CommentConfiguration());

        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasData(new User(Guid.Parse("635ECF0D-A569-4E94-9C14-29F5D3FCF220"))
            {
                Name = "Admin",
                Email = "admin@example.com",
                Password = this.passwordHasher.HashPassword("test"),
                Role = Role.Administrator,
                IsActive = true,
                IsDefault = true
            }, new User(Guid.Parse("B5D61694-B355-46DC-AFA1-2C385D2B3A7D"))
            {
                Name = "Manager",
                Email = "manager@example.com",
                Password = this.passwordHasher.HashPassword("test"),
                Role = Role.Manager,
                IsActive = true,
                IsDefault = false
            }, new User(Guid.Parse("C04AB03D-49F1-4E03-9806-7282F9F61C54"))
            {
                Name = "User",
                Email = "user@example.com",
                Password = this.passwordHasher.HashPassword("test"),
                Role = Role.User,
                IsActive = true,
                IsDefault = false
            });

        modelBuilder.Entity<Project>()
            .HasData(new Project(Guid.Parse("3F944F0F-33F2-4024-8D05-CFA211E61989"))
            {
                Name = "First project",
                IsActive = true
            }, new Project(Guid.Parse("E6173161-BF7E-415F-AAE0-A2243265617D"))
            {
                Name = "Second project",
                IsActive = true
            });

        modelBuilder.Entity<Category>()
            .HasData(new Category(Guid.Parse("6B84C457-A617-4306-BAA4-D78DA2146A3B"))
            {
                Name = "Bug",
                IsActive = true
            }, new Category(Guid.Parse("C1DC5DDC-8A36-4B15-9A61-EAE62418F2B0"))
            {
                Name = "Task",
                IsActive = true
            });

        modelBuilder.Entity<Priority>()
            .HasData(new Priority(Guid.Parse("A3BC5378-C993-45CB-9858-88471A75FB01"))
            {
                Name = "High",
                Weight = 1,
                IsActive = true
            }, new Priority(Guid.Parse("F8E2A9A0-A3D8-48D9-B30D-CDB62645A8ED"))
            {
                Name = "Medium",
                Weight = 2,
                IsActive = true
            }, new Priority(Guid.Parse("266ABBF5-DC7F-4052-9AB2-DE3FAF789D1F"))
            {
                Name = "Low",
                Weight = 3,
                IsActive = true
            });
    }
}
