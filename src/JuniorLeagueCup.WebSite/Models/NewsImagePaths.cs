namespace JuniorLeagueCup.WebSite.Models;

public static class NewsImagePaths
{
    public const string Default = "/images/hero-bg.jpg";

    public static string Resolve(string? imageUrl) =>
        string.IsNullOrWhiteSpace(imageUrl) ? Default : imageUrl;
}
