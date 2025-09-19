using Microsoft.EntityFrameworkCore;
using PersonalWebsite.Server.Models;
using System.Text.Json;
using System;

namespace PersonalWebsite.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<PageComponent> PageComponents { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Configure relationships
            modelBuilder.Entity<PageComponent>()
                .HasOne(pc => pc.Page)
                .WithMany()
                .HasForeignKey(pc => pc.PageId)
                .OnDelete(DeleteBehavior.Cascade);
                
            // Add indexes
            modelBuilder.Entity<Page>()
                .HasIndex(p => p.Slug)
                .IsUnique();
                
            // Seed admin user
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "admin",
                    // This is a hashed password for "admin123" - in production, use a proper password hashing service
                    PasswordHash = "AQAAAAIAAYagAAAAELTsZ4S3rD1+Qm+QFQgYZdtLGFUGKQj8yAMRwRgdJvN3FJMJnCJfYXFcVXp+YOqZdw==",
                    Email = "admin@example.com",
                    FirstName = "Admin",
                    LastName = "User",
                    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                }
            );
            
            // Create default layout for home page
            var defaultLayout = new Dictionary<string, object>
            {
                { "sections", new[] 
                    {
                        new { id = "hero", name = "Hero Section", type = "hero" },
                        new { id = "about", name = "About Section", type = "content" },
                        new { id = "portfolio", name = "Portfolio Section", type = "grid" }
                    }
                }
            };
            
            // Seed default home page with fixed dates
            modelBuilder.Entity<Page>().HasData(
                new Page
                {
                    Id = 1,
                    Title = "Home",
                    Slug = "home",
                    Description = "Welcome to Alex Morgan's personal website",
                    Content = "<h1>Welcome to My Personal Website</h1><p>This is the home page content.</p>",
                    IsPublished = true,
                    PublishedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    Order = 1,
                    ShowInNavigation = true,
                    LayoutJson = JsonSerializer.Serialize(defaultLayout)
                }
            );
        }
    }
}