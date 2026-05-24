using Microsoft.EntityFrameworkCore;
using JuniorLeagueCup.WebSite.Models;

namespace JuniorLeagueCup.WebSite.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<NewsArticle> NewsArticles => Set<NewsArticle>();
    public DbSet<AdminUser> AdminUsers => Set<AdminUser>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<NewsArticle>(e =>
        {
            e.HasIndex(x => x.Slug).IsUnique();
            e.Property(x => x.Title).HasMaxLength(200);
            e.Property(x => x.Summary).HasMaxLength(500);
            e.Property(x => x.Category).HasMaxLength(50);
            e.Property(x => x.ImageUrl).HasMaxLength(500);
            e.Property(x => x.Slug).HasMaxLength(220);
        });

        modelBuilder.Entity<AdminUser>(e =>
        {
            e.HasIndex(x => x.Username).IsUnique();
            e.Property(x => x.Username).HasMaxLength(100);
            e.Property(x => x.PasswordHash).HasMaxLength(500);
        });
    }
}
