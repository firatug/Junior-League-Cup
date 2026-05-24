namespace JuniorLeagueCup.WebSite.Models;

public class NewsArticle
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string Category { get; set; } = "Haber";
    public string ImageUrl { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public bool IsPublished { get; set; }
    public bool ShowOnHomepage { get; set; } = true;
    public DateTime PublishedAt { get; set; } = DateTime.UtcNow;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
