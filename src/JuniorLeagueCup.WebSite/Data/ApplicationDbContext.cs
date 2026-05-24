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
            e.Property(x => x.TitleEn).HasMaxLength(200);
            e.Property(x => x.TitleEs).HasMaxLength(200);
            e.Property(x => x.TitleDe).HasMaxLength(200);
            e.Property(x => x.Summary).HasMaxLength(500);
            e.Property(x => x.SummaryEn).HasMaxLength(500);
            e.Property(x => x.SummaryEs).HasMaxLength(500);
            e.Property(x => x.SummaryDe).HasMaxLength(500);
            e.Property(x => x.ContentEn).HasColumnType("nvarchar(max)");
            e.Property(x => x.ContentEs).HasColumnType("nvarchar(max)");
            e.Property(x => x.ContentDe).HasColumnType("nvarchar(max)");
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
