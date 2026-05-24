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
            new()
            {
                Title = "Junior League Cup 2026 Sezonu Resmen Başlıyor",
                Summary = "Türkiye genelinde bölgesel seçme takvimi ve Antalya final hazırlıkları duyuruldu.",
                Content = """
                    Junior League Cup 2026 sezonu, Türkiye'nin dört bir yanından genç futbolcu ve kulüpleri bir araya getiriyor.
                    Bölgesel seçmeler İstanbul, Ankara, İzmir, Bursa, Adana ve Gaziantep'te oynanacak; finale kalan takımlar
                    Antalya'da büyük finalde karşılaşacak. Başvurular www.juniorleaguecup.com üzerinden alınmaya devam ediyor.
                    """,
                Category = "Haber",
                ImageUrl = "/images/hero-bg.jpg",
                Slug = "junior-league-cup-2026-sezonu-basliyor",
                IsPublished = true,
                ShowOnHomepage = true,
                PublishedAt = now.AddDays(-5),
                CreatedAt = now.AddDays(-5)
            },
            new()
            {
                Title = "Road to Antalya: Yolculuk Başlıyor",
                Summary = "Organizasyon tanıtım videosu ve marka hikâyesi yayında — genç yeteneklerin yolu Antalya'ya uzanıyor.",
                Content = """
                    Road to Antalya serisi, Junior League Cup deneyimini sahaya ve soyunma odasına taşıyor.
                    Tanıtım filminde organizasyonun profesyonel altyapısı, scout sistemi ve final atmosferi öne çıkıyor.
                    Yeni bölümler medya sayfamızdan takip edilebilir.
                    """,
                Category = "Video",
                ImageUrl = "/images/ronaldinho.png",
                Slug = "road-to-antalya-yolculuk-basliyor",
                IsPublished = true,
                ShowOnHomepage = true,
                PublishedAt = now.AddDays(-3),
                CreatedAt = now.AddDays(-3)
            },
            new()
            {
                Title = "Kulüpler JLC Deneyimini Anlatıyor",
                Summary = "Partner kulüpler profesyonel organizasyon, görünürlük ve scout fırsatlarını değerlendirdi.",
                Content = """
                    Geçtiğimiz sezonlara katılan kulüp yetkilileri, Junior League Cup'ın altyapılarına sağladığı
                    ulusal görünürlük ve scout ağına dikkat çekti. Teknik direktörler, genç oyuncuların
                    baskı altında performans gösterme fırsatı bulduğunu vurguladı.
                    """,
                Category = "Röportaj",
                ImageUrl = "/images/logo.png",
                Slug = "kulupler-jlc-deneyimini-anlatiyor",
                IsPublished = true,
                ShowOnHomepage = true,
                PublishedAt = now.AddDays(-2),
                CreatedAt = now.AddDays(-2)
            },
            new()
            {
                Title = "Antalya Finali İçin Konaklama ve Transfer Duyurusu",
                Summary = "Final haftası için anlaşmalı oteller ve transfer planlaması açıklandı.",
                Content = """
                    Antalya finaline katılacak takımlar için Kremlin Palace, Limak Lara ve Fame Lara başta olmak üzere
                    anlaşmalı konaklama seçenekleri duyuruldu. Organizasyon komitesi, havalimanı transferleri ve
                    maç günü programı hakkında detaylı bilgiyi takımlarla paylaşmaya başladı.
                    """,
                Category = "Duyuru",
                ImageUrl = "/images/hero-bg.jpg",
                Slug = "antalya-finali-konaklama-duyurusu",
                IsPublished = true,
                ShowOnHomepage = true,
                PublishedAt = now.AddDays(-1),
                CreatedAt = now.AddDays(-1)
            }
        };

        db.NewsArticles.AddRange(articles);
        await db.SaveChangesAsync();
    }

    public static async Task FixArticleImagesAsync(ApplicationDbContext db)
    {
        var changed = false;
        foreach (var article in await db.NewsArticles.ToListAsync())
        {
            var needsFix = string.IsNullOrWhiteSpace(article.ImageUrl) ||
                           article.ImageUrl.Contains("unsplash.com", StringComparison.OrdinalIgnoreCase);

            if (!needsFix)
                continue;

            article.ImageUrl = DefaultImages.TryGetValue(article.Slug, out var path)
                ? path
                : NewsImagePaths.Default;
            changed = true;
        }

        if (changed)
            await db.SaveChangesAsync();
    }
}
