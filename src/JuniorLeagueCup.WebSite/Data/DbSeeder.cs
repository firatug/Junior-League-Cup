using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using JuniorLeagueCup.WebSite.Models;

namespace JuniorLeagueCup.WebSite.Data;

public static class DbSeeder
{
    public const string DefaultAdminUsername = "admin";
    public const string DefaultAdminPassword = "JlcAdmin2026!";

    private static readonly Dictionary<string, string> DefaultImages = new()
    {
        ["junior-league-cup-2026-sezonu-basliyor"] = "/images/hero-bg.jpg",
        ["road-to-antalya-yolculuk-basliyor"] = "/images/ronaldinho.png",
        ["kulupler-jlc-deneyimini-anlatiyor"] = "/images/logo.png",
        ["antalya-finali-konaklama-duyurusu"] = "/images/hero-bg.jpg"
    };

    private sealed record ArticleTranslation(string Title, string Summary, string Content);

    private static readonly Dictionary<string, ArticleTranslation> EnglishArticles = new()
    {
        ["junior-league-cup-2026-sezonu-basliyor"] = new(
            "Junior League Cup 2026 Season Officially Begins",
            "Regional selection schedule across Turkey and Antalya final preparations have been announced.",
            """
            The Junior League Cup 2026 season brings together young players and clubs from across Turkey.
            Regional selections will be held in Istanbul, Ankara, Izmir, Bursa, Adana and Gaziantep; qualifying teams
            will meet in the grand final in Antalya. Applications continue at www.juniorleaguecup.com.
            """),
        ["road-to-antalya-yolculuk-basliyor"] = new(
            "Road to Antalya: The Journey Begins",
            "Organisation promo video and brand story now live — young talents on the road to Antalya.",
            """
            The Road to Antalya series brings the Junior League Cup experience onto the pitch and into the locker room.
            The promo film highlights professional infrastructure, the scout system and the final atmosphere.
            New episodes can be followed on our news page.
            """),
        ["kulupler-jlc-deneyimini-anlatiyor"] = new(
            "Clubs Share Their JLC Experience",
            "Partner clubs evaluated professional organisation, visibility and scout opportunities.",
            """
            Club officials from previous seasons highlighted the national visibility and scout network
            Junior League Cup provides for academies. Coaches emphasised the opportunity for young players
            to perform under pressure.
            """),
        ["antalya-finali-konaklama-duyurusu"] = new(
            "Accommodation and Transfer Announcement for Antalya Final",
            "Partner hotels and transfer planning announced for final week.",
            """
            Partner accommodation options including Kremlin Palace, Limak Lara and Fame Lara have been announced
            for teams attending the Antalya final. The organising committee has begun sharing detailed information
            on airport transfers and match-day schedules with participating teams.
            """)
    };

    private static readonly Dictionary<string, ArticleTranslation> SpanishArticles = new()
    {
        ["junior-league-cup-2026-sezonu-basliyor"] = new(
            "La Temporada Junior League Cup 2026 Comienza Oficialmente",
            "Se ha anunciado el calendario de selecciones regionales en Turquía y los preparativos para la final en Antalya.",
            """
            La temporada Junior League Cup 2026 reúne a jóvenes jugadores y clubes de toda Turquía.
            Las selecciones regionales se celebrarán en Estambul, Ankara, Esmirna, Bursa, Adana y Gaziantep; los equipos clasificados
            se encontrarán en la gran final en Antalya. Las solicitudes continúan en www.juniorleaguecup.com.
            """),
        ["road-to-antalya-yolculuk-basliyor"] = new(
            "Road to Antalya: Comienza el Viaje",
            "Video promocional y la historia de la marca ya disponibles — jóvenes talentos camino a Antalya.",
            """
            La serie Road to Antalya lleva la experiencia Junior League Cup al campo y al vestuario.
            El vídeo promocional destaca la infraestructura profesional, el sistema de scouting y el ambiente de la final.
            Los nuevos episodios pueden seguirse en nuestra página de noticias.
            """),
        ["kulupler-jlc-deneyimini-anlatiyor"] = new(
            "Los Clubes Comparten su Experiencia JLC",
            "Los clubes socios valoraron la organización profesional, la visibilidad y las oportunidades de scouting.",
            """
            Los responsables de clubes de temporadas anteriores destacaron la visibilidad nacional y la red de scouts
            que Junior League Cup ofrece a las academias. Los entrenadores subrayaron la oportunidad para que los jóvenes
            demuestren su rendimiento bajo presión.
            """),
        ["antalya-finali-konaklama-duyurusu"] = new(
            "Anuncio de Alojamiento y Traslados para la Final de Antalya",
            "Hoteles asociados y planificación de traslados anunciados para la semana de la final.",
            """
            Se han anunciado opciones de alojamiento asociadas, incluidos Kremlin Palace, Limak Lara y Fame Lara,
            para los equipos que asistan a la final en Antalya. El comité organizador ha comenzado a compartir información detallada
            sobre traslados desde el aeropuerto y el calendario de los días de partido.
            """)
    };

    private static readonly Dictionary<string, ArticleTranslation> GermanArticles = new()
    {
        ["junior-league-cup-2026-sezonu-basliyor"] = new(
            "Junior League Cup Saison 2026 Beginnt Offiziell",
            "Der regionale Auswahlspielplan in der Türkei und die Vorbereitungen für das Antalya-Finale wurden bekannt gegeben.",
            """
            Die Junior League Cup Saison 2026 bringt junge Spieler und Vereine aus der gesamten Türkei zusammen.
            Regionale Auswahlspiele finden in Istanbul, Ankara, Izmir, Bursa, Adana und Gaziantep statt; qualifizierte Teams
            treffen sich im großen Finale in Antalya. Bewerbungen laufen weiter unter www.juniorleaguecup.com.
            """),
        ["road-to-antalya-yolculuk-basliyor"] = new(
            "Road to Antalya: Die Reise Beginnt",
            "Organisations-Promovideo und Markengeschichte online — junge Talente auf dem Weg nach Antalya.",
            """
            Die Road to Antalya Serie bringt das Junior League Cup Erlebnis auf den Platz und in die Kabine.
            Der Promofilm hebt die professionelle Infrastruktur, das Scout-System und die Final-Atmosphäre hervor.
            Neue Folgen sind auf unserer Nachrichtenseite verfügbar.
            """),
        ["kulupler-jlc-deneyimini-anlatiyor"] = new(
            "Vereine Berichten über ihre JLC-Erfahrung",
            "Partnervvereine bewerteten professionelle Organisation, Sichtbarkeit und Scout-Möglichkeiten.",
            """
            Vereinsvertreter aus vergangenen Saisons hoben die nationale Sichtbarkeit und das Scout-Netzwerk hervor,
            das Junior League Cup Akademien bietet. Trainer betonten die Chance für junge Spieler,
            unter Druck zu performen.
            """),
        ["antalya-finali-konaklama-duyurusu"] = new(
            "Unterkunfts- und Transferankündigung für das Antalya-Finale",
            "Partnerhotels und Transferplanung für die Finalwoche bekannt gegeben.",
            """
            Partnerunterkünfte einschließlich Kremlin Palace, Limak Lara und Fame Lara wurden
            für Teams beim Antalya-Finale angekündigt. Das Organisationskomitee hat begonnen, detaillierte Informationen
            zu Flughafentransfers und Spieltagsplänen mit teilnehmenden Teams zu teilen.
            """)
    };

    public static async Task SeedAsync(ApplicationDbContext db)
    {
        if (!await db.AdminUsers.AnyAsync())
        {
            var hasher = new PasswordHasher<AdminUser>();
            var admin = new AdminUser { Username = DefaultAdminUsername };
            admin.PasswordHash = hasher.HashPassword(admin, DefaultAdminPassword);
            db.AdminUsers.Add(admin);
            await db.SaveChangesAsync();
        }

        if (await db.NewsArticles.AnyAsync())
            return;

        var now = DateTime.UtcNow;
        var articles = new List<NewsArticle>
        {
            CreateArticle(
                "Junior League Cup 2026 Sezonu Resmen Başlıyor",
                "Türkiye genelinde bölgesel seçme takvimi ve Antalya final hazırlıkları duyuruldu.",
                """
                Junior League Cup 2026 sezonu, Türkiye'nin dört bir yanından genç futbolcu ve kulüpleri bir araya getiriyor.
                Bölgesel seçmeler İstanbul, Ankara, İzmir, Bursa, Adana ve Gaziantep'te oynanacak; finale kalan takımlar
                Antalya'da büyük finalde karşılaşacak. Başvurular www.juniorleaguecup.com üzerinden alınmaya devam ediyor.
                """,
                "junior-league-cup-2026-sezonu-basliyor", "Haber", "/images/hero-bg.jpg", now.AddDays(-5)),
            CreateArticle(
                "Road to Antalya: Yolculuk Başlıyor",
                "Organizasyon tanıtım videosu ve marka hikâyesi yayında — genç yeteneklerin yolu Antalya'ya uzanıyor.",
                """
                Road to Antalya serisi, Junior League Cup deneyimini sahaya ve soyunma odasına taşıyor.
                Tanıtım filminde organizasyonun profesyonel altyapısı, scout sistemi ve final atmosferi öne çıkıyor.
                Yeni bölümler medya sayfamızdan takip edilebilir.
                """,
                "road-to-antalya-yolculuk-basliyor", "Video", "/images/ronaldinho.png", now.AddDays(-3)),
            CreateArticle(
                "Kulüpler JLC Deneyimini Anlatıyor",
                "Partner kulüpler profesyonel organizasyon, görünürlük ve scout fırsatlarını değerlendirdi.",
                """
                Geçtiğimiz sezonlara katılan kulüp yetkilileri, Junior League Cup'ın altyapılarına sağladığı
                ulusal görünürlük ve scout ağına dikkat çekti. Teknik direktörler, genç oyuncuların
                baskı altında performans gösterme fırsatı bulduğunu vurguladı.
                """,
                "kulupler-jlc-deneyimini-anlatiyor", "Röportaj", "/images/logo.png", now.AddDays(-2)),
            CreateArticle(
                "Antalya Finali İçin Konaklama ve Transfer Duyurusu",
                "Final haftası için anlaşmalı oteller ve transfer planlaması açıklandı.",
                """
                Antalya finaline katılacak takımlar için Kremlin Palace, Limak Lara ve Fame Lara başta olmak üzere
                anlaşmalı konaklama seçenekleri duyuruldu. Organizasyon komitesi, havalimanı transferleri ve
                maç günü programı hakkında detaylı bilgiyi takımlarla paylaşmaya başladı.
                """,
                "antalya-finali-konaklama-duyurusu", "Duyuru", "/images/hero-bg.jpg", now.AddDays(-1))
        };

        db.NewsArticles.AddRange(articles);
        await db.SaveChangesAsync();
    }

    private static NewsArticle CreateArticle(string title, string summary, string content, string slug, string category, string image, DateTime published)
    {
        EnglishArticles.TryGetValue(slug, out var en);
        SpanishArticles.TryGetValue(slug, out var es);
        GermanArticles.TryGetValue(slug, out var de);
        return new NewsArticle
        {
            Title = title,
            TitleEn = en?.Title,
            TitleEs = es?.Title,
            TitleDe = de?.Title,
            Summary = summary,
            SummaryEn = en?.Summary,
            SummaryEs = es?.Summary,
            SummaryDe = de?.Summary,
            Content = content,
            ContentEn = en?.Content,
            ContentEs = es?.Content,
            ContentDe = de?.Content,
            Category = category,
            ImageUrl = image,
            Slug = slug,
            IsPublished = true,
            ShowOnHomepage = true,
            PublishedAt = published,
            CreatedAt = published
        };
    }

    public static async Task FixArticleImagesAsync(ApplicationDbContext db)
    {
        var changed = false;
        foreach (var article in await db.NewsArticles.ToListAsync())
        {
            if (string.IsNullOrWhiteSpace(article.ImageUrl) ||
                article.ImageUrl.Contains("unsplash.com", StringComparison.OrdinalIgnoreCase))
            {
                article.ImageUrl = DefaultImages.TryGetValue(article.Slug, out var path)
                    ? path
                    : NewsImagePaths.Default;
                changed = true;
            }
        }

        if (changed)
            await db.SaveChangesAsync();
    }

    public static async Task FixEnglishArticlesAsync(ApplicationDbContext db) =>
        await FixTranslationSetAsync(db, EnglishArticles, static (article, t) =>
        {
            var changed = false;
            if (string.IsNullOrWhiteSpace(article.TitleEn)) { article.TitleEn = t.Title; changed = true; }
            if (string.IsNullOrWhiteSpace(article.SummaryEn)) { article.SummaryEn = t.Summary; changed = true; }
            if (string.IsNullOrWhiteSpace(article.ContentEn)) { article.ContentEn = t.Content; changed = true; }
            return changed;
        });

    public static async Task FixTranslatedArticlesAsync(ApplicationDbContext db)
    {
        await FixTranslationSetAsync(db, SpanishArticles, static (article, t) =>
        {
            var changed = false;
            if (string.IsNullOrWhiteSpace(article.TitleEs)) { article.TitleEs = t.Title; changed = true; }
            if (string.IsNullOrWhiteSpace(article.SummaryEs)) { article.SummaryEs = t.Summary; changed = true; }
            if (string.IsNullOrWhiteSpace(article.ContentEs)) { article.ContentEs = t.Content; changed = true; }
            return changed;
        });
        await FixTranslationSetAsync(db, GermanArticles, static (article, t) =>
        {
            var changed = false;
            if (string.IsNullOrWhiteSpace(article.TitleDe)) { article.TitleDe = t.Title; changed = true; }
            if (string.IsNullOrWhiteSpace(article.SummaryDe)) { article.SummaryDe = t.Summary; changed = true; }
            if (string.IsNullOrWhiteSpace(article.ContentDe)) { article.ContentDe = t.Content; changed = true; }
            return changed;
        });
    }

    private static async Task FixTranslationSetAsync(
        ApplicationDbContext db,
        Dictionary<string, ArticleTranslation> translations,
        Func<NewsArticle, ArticleTranslation, bool> tryApply)
    {
        var changed = false;
        foreach (var article in await db.NewsArticles.ToListAsync())
        {
            if (!translations.TryGetValue(article.Slug, out var translation))
                continue;
            changed |= tryApply(article, translation);
        }

        if (changed)
            await db.SaveChangesAsync();
    }
}
