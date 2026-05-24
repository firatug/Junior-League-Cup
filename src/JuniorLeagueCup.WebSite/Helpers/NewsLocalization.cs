using JuniorLeagueCup.WebSite.Localization;
using JuniorLeagueCup.WebSite.Models;

namespace JuniorLeagueCup.WebSite.Helpers;

public static class NewsLocalization
{
    public static string GetTitle(NewsArticle article) =>
        Pick(article.Title, article.TitleEn, article.TitleEs, article.TitleDe);

    public static string GetSummary(NewsArticle article) =>
        Pick(article.Summary, article.SummaryEn, article.SummaryEs, article.SummaryDe);

    public static string GetContent(NewsArticle article) =>
        Pick(article.Content, article.ContentEn, article.ContentEs, article.ContentDe);

    public static string GetCategory(NewsArticle article) =>
        LocaleHelper.Current switch
        {
            "en" => TranslateCategory(article.Category, EnCategories),
            "es" => TranslateCategory(article.Category, EsCategories),
            "de" => TranslateCategory(article.Category, DeCategories),
            _ => article.Category
        };

    private static string Pick(string tr, string? en, string? es, string? de) =>
        LocaleHelper.Current switch
        {
            "en" when !string.IsNullOrWhiteSpace(en) => en!,
            "es" when !string.IsNullOrWhiteSpace(es) => es!,
            "de" when !string.IsNullOrWhiteSpace(de) => de!,
            _ => tr
        };

    private static string TranslateCategory(string category, Dictionary<string, string> map) =>
        map.TryGetValue(category, out var translated) ? translated : category;

    private static readonly Dictionary<string, string> EnCategories = new()
    {
        ["Haber"] = "News",
        ["Video"] = "Video",
        ["Röportaj"] = "Interview",
        ["Foto Galeri"] = "Photo Gallery",
        ["Duyuru"] = "Announcement",
        ["Sponsor İçeriği"] = "Sponsor Content"
    };

    private static readonly Dictionary<string, string> EsCategories = new()
    {
        ["Haber"] = "Noticia",
        ["Video"] = "Video",
        ["Röportaj"] = "Entrevista",
        ["Foto Galeri"] = "Galería de Fotos",
        ["Duyuru"] = "Anuncio",
        ["Sponsor İçeriği"] = "Contenido de Patrocinio"
    };

    private static readonly Dictionary<string, string> DeCategories = new()
    {
        ["Haber"] = "Nachricht",
        ["Video"] = "Video",
        ["Röportaj"] = "Interview",
        ["Foto Galeri"] = "Fotogalerie",
        ["Duyuru"] = "Ankündigung",
        ["Sponsor İçeriği"] = "Sponsor-Inhalt"
    };
}
