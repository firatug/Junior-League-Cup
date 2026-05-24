namespace JuniorLeagueCup.WebSite.Models;

public class NewsArticle
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? TitleEn { get; set; }
    public string? TitleEs { get; set; }
    public string? TitleDe { get; set; }
    public string Summary { get; set; } = string.Empty;
    public string? SummaryEn { get; set; }
    public string? SummaryEs { get; set; }
    public string? SummaryDe { get; set; }
    public string Content { get; set; } = string.Empty;
    public string? ContentEn { get; set; }
    public string? ContentEs { get; set; }
    public string? ContentDe { get; set; }
    public string Category { get; set; } = "Haber";
    public string ImageUrl { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public bool IsPublished { get; set; }
    public bool ShowOnHomepage { get; set; } = true;
    public DateTime PublishedAt { get; set; } = DateTime.UtcNow;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
