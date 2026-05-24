using System.Globalization;

namespace JuniorLeagueCup.WebSite.Localization;

public static class LocaleHelper
{
    public static string Current =>
        CultureInfo.CurrentUICulture.TwoLetterISOLanguageName.ToLowerInvariant();

    public static string Pick(string tr, string en, string es, string de) =>
        Current switch
        {
            "en" => en,
            "es" => es,
            "de" => de,
            _ => tr
        };

    public static string HtmlLang => Current switch
    {
        "en" => "en",
        "es" => "es",
        "de" => "de",
        _ => "tr"
    };

    public static CultureInfo DateCulture => Current switch
    {
        "en" => new CultureInfo("en-US"),
        "es" => new CultureInfo("es-ES"),
        "de" => new CultureInfo("de-DE"),
        _ => new CultureInfo("tr-TR")
    };

    public static string CurrentLangLabel => Current switch
    {
        "en" => "EN",
        "es" => "ES",
        "de" => "DE",
        _ => "TR"
    };
}
