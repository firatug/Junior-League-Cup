using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace JuniorLeagueCup.WebSite.Data;

public static partial class SlugHelper
{
    public static string ToSlug(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            return "haber";

        var normalized = title.ToLowerInvariant().Normalize(NormalizationForm.FormD);
        var sb = new StringBuilder();
        foreach (var c in normalized)
        {
            if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                sb.Append(c);
        }

        var slug = sb.ToString();
        slug = slug
            .Replace('ı', 'i').Replace('ğ', 'g').Replace('ü', 'u')
            .Replace('ş', 's').Replace('ö', 'o').Replace('ç', 'c')
            .Replace('İ', 'i').Replace('Ğ', 'g').Replace('Ü', 'u')
            .Replace('Ş', 's').Replace('Ö', 'o').Replace('Ç', 'c');

        slug = NonAlphanumeric().Replace(slug, "-");
        slug = MultiDash().Replace(slug, "-").Trim('-');
        return string.IsNullOrEmpty(slug) ? "haber" : slug;
    }

    [GeneratedRegex(@"[^a-z0-9\-]", RegexOptions.Compiled)]
    private static partial Regex NonAlphanumeric();

    [GeneratedRegex(@"-{2,}", RegexOptions.Compiled)]
    private static partial Regex MultiDash();
}
