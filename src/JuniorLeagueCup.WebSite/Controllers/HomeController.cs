using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JuniorLeagueCup.WebSite.Data;
using JuniorLeagueCup.WebSite.Models;
using JuniorLeagueCup.WebSite.Services;

namespace JuniorLeagueCup.WebSite.Controllers;

public class HomeController(ApplicationDbContext db, IEmailService emailService, ILogger<HomeController> logger) : Controller
{
    public async Task<IActionResult> Index()
    {
        var news = await GetPublishedNewsAsync(homepageOnly: true, take: 3);
        return View(news);
    }

    [Route("jlc-nedir")]
    public IActionResult JlcNedir() => View();

    [Route("bolgesel-secmeler")]
    public IActionResult BolgeselSecmeler() => View();

    [Route("antalya-finali")]
    public IActionResult AntalyaFinali() => View();

    [Route("canli-skor")]
    public IActionResult CanliSkor() => View();

    [Route("takim-basvurusu")]
    [HttpGet]
    public IActionResult TakimBasvurusu() => View(new TakimBasvurusuFormModel());

    [Route("takim-basvurusu")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> TakimBasvurusuPost(TakimBasvurusuFormModel model)
    {
        if (!ModelState.IsValid)
            return View("TakimBasvurusu", model);

        var sent = await emailService.SendFormEmailAsync(
            "Junior League Cup — Yeni Takım Başvurusu",
            [
                ("Kulüp / Futbol Okulu", model.ClubName),
                ("Yetkili Kişi", model.ContactName),
                ("Telefon", model.Phone),
                ("E-posta", model.Email),
                ("Şehir", model.City),
                ("Yaş Grubu", model.AgeGroup),
                ("Oyuncu Sayısı", model.PlayerCount?.ToString()),
                ("Önceki Organizasyonlar", model.PastEvents),
                ("Notlar", model.Notes)
            ],
            model.Email,
            model.ContactName);

        if (!sent)
        {
            ModelState.AddModelError(string.Empty, "Başvurunuz gönderilemedi. Lütfen info@juniorleaguecup.com adresine yazın veya 0242 324 03 54 numarayı arayın.");
            return View("TakimBasvurusu", model);
        }

        TempData["FormThanks"] = FormThankYouTypes.TakimBasvurusu;
        return RedirectToAction(nameof(Tesekkur));
    }

    [Route("veli-oyuncu-deneyimi")]
    public IActionResult VeliOyuncuDeneyimi() => View();

    [Route("scout-sistemi")]
    public IActionResult ScoutSistemi() => View();

    [Route("sponsorluk")]
    [HttpGet]
    public IActionResult Sponsorluk() => View(new SponsorlukFormModel());

    [Route("sponsorluk")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SponsorlukPost(SponsorlukFormModel model)
    {
        if (!ModelState.IsValid)
            return View("Sponsorluk", model);

        var sent = await emailService.SendFormEmailAsync(
            "Junior League Cup — Sponsorluk Talebi",
            [
                ("Firma Adı", model.Company),
                ("Yetkili Kişi", model.Contact),
                ("Telefon", model.Phone),
                ("E-posta", model.Email),
                ("Sponsorluk Türü", model.SponsorType),
                ("Mesaj", model.Message)
            ],
            model.Email,
            model.Contact);

        if (!sent)
        {
            ModelState.AddModelError(string.Empty, "Talebiniz gönderilemedi. Lütfen info@juniorleaguecup.com adresine yazın.");
            return View("Sponsorluk", model);
        }

        TempData["FormThanks"] = FormThankYouTypes.Sponsorluk;
        return RedirectToAction(nameof(Tesekkur));
    }

    [Route("medya")]
    public async Task<IActionResult> Medya()
    {
        var news = await GetPublishedNewsAsync();
        return View(news);
    }

    [Route("haber/{slug}")]
    public async Task<IActionResult> HaberDetay(string slug)
    {
        try
        {
            var article = await db.NewsArticles
                .FirstOrDefaultAsync(n => n.Slug == slug && n.IsPublished);
            if (article is null)
                return NotFound();
            return View(article);
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Haber detayı yüklenemedi: {Slug}", slug);
            return NotFound();
        }
    }

    [Route("program-fikstur")]
    public IActionResult ProgramFikstur() => View();

    [Route("takimlar")]
    public IActionResult Takimlar() => View();

    [Route("oteller")]
    public IActionResult Oteller() => View();

    [Route("hakkimizda")]
    public IActionResult Hakkimizda() => View();

    [Route("kvkk")]
    public IActionResult Kvkk() => View();

    [Route("cerez-politikasi")]
    public IActionResult CerezPolitikasi() => View();

    [Route("kullanim-sartlari")]
    public IActionResult KullanimSartlari() => View();

    [Route("iletisim")]
    [HttpGet]
    public IActionResult Iletisim() => View(new IletisimFormModel());

    [Route("tesekkur")]
    [HttpGet]
    public IActionResult Tesekkur()
    {
        var formType = TempData["FormThanks"] as string;
        if (string.IsNullOrEmpty(formType) || !FormThankYouTypes.All.Contains(formType))
            return RedirectToAction(nameof(Index));

        ViewData["FormType"] = formType;
        return View();
    }

    [Route("iletisim")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> IletisimPost(IletisimFormModel model)
    {
        if (!ModelState.IsValid)
            return View("Iletisim", model);

        var sent = await emailService.SendFormEmailAsync(
            $"Junior League Cup — İletişim: {model.Subject}",
            [
                ("Ad Soyad", model.FullName),
                ("Telefon", model.Phone),
                ("E-posta", model.Email),
                ("Konu", model.Subject),
                ("Mesaj", model.Message)
            ],
            model.Email,
            model.FullName);

        if (!sent)
        {
            ModelState.AddModelError(string.Empty, "Mesajınız gönderilemedi. Lütfen info@juniorleaguecup.com adresine yazın.");
            return View("Iletisim", model);
        }

        TempData["FormThanks"] = FormThankYouTypes.Iletisim;
        return RedirectToAction(nameof(Tesekkur));
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    private async Task<List<NewsArticle>> GetPublishedNewsAsync(bool homepageOnly = false, int? take = null)
    {
        try
        {
            var query = db.NewsArticles.Where(n => n.IsPublished);
            if (homepageOnly)
                query = query.Where(n => n.ShowOnHomepage);

            query = query.OrderByDescending(n => n.PublishedAt);
            if (take.HasValue)
                query = query.Take(take.Value);

            return await query.ToListAsync();
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Haberler veritabanından yüklenemedi.");
            return [];
        }
    }
}
