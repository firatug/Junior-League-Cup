using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JuniorLeagueCup.WebSite.Data;
using JuniorLeagueCup.WebSite.Models;
using JuniorLeagueCup.WebSite.Services;

namespace JuniorLeagueCup.WebSite.Controllers;

[Route("admin")]
public class AdminController(ApplicationDbContext db, NewsImageService imageService) : Controller
{
    [HttpGet("giris")]
    [AllowAnonymous]
    public IActionResult Login(string? returnUrl = null)
    {
        if (User.Identity?.IsAuthenticated == true)
            return RedirectToAction(nameof(Index));

        ViewData["ReturnUrl"] = returnUrl;
        return View(new AdminLoginViewModel());
    }

    [HttpPost("giris")]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(AdminLoginViewModel model, string? returnUrl = null)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = await db.AdminUsers.FirstOrDefaultAsync(u => u.Username == model.Username);
        if (user is null)
        {
            ModelState.AddModelError(string.Empty, "Kullanıcı adı veya şifre hatalı.");
            return View(model);
        }

        var hasher = new PasswordHasher<AdminUser>();
        var result = hasher.VerifyHashedPassword(user, user.PasswordHash, model.Password);
        if (result == PasswordVerificationResult.Failed)
        {
            ModelState.AddModelError(string.Empty, "Kullanıcı adı veya şifre hatalı.");
            return View(model);
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.Username),
            new(ClaimTypes.NameIdentifier, user.Id.ToString())
        };
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(identity),
            new AuthenticationProperties { IsPersistent = true });

        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            return Redirect(returnUrl);

        return RedirectToAction(nameof(Index));
    }

    [HttpPost("cikis")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction(nameof(Login));
    }

    [HttpGet("")]
    [Authorize]
    public async Task<IActionResult> Index()
    {
        var news = await db.NewsArticles
            .OrderByDescending(n => n.PublishedAt)
            .ToListAsync();
        return View(news);
    }

    [HttpGet("haber/yeni")]
    [Authorize]
    public IActionResult CreateNews() => View("EditNews", new NewsEditViewModel());

    [HttpPost("haber/yeni")]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateNews(NewsEditViewModel model)
    {
        if (model.ImageFile is { Length: > 0 })
        {
            var upload = await imageService.SaveAsync(model.ImageFile);
            if (!upload.Success)
            {
                ModelState.AddModelError(nameof(model.ImageFile), upload.Error ?? "Görsel yüklenemedi.");
                return View("EditNews", model);
            }
            model.ImageUrl = upload.Path;
        }

        if (!ModelState.IsValid)
            return View("EditNews", model);

        var slug = await EnsureUniqueSlugAsync(SlugHelper.ToSlug(model.Title));
        var article = new NewsArticle
        {
            Title = model.Title.Trim(),
            Summary = model.Summary.Trim(),
            Content = model.Content.Trim(),
            Category = model.Category,
            ImageUrl = model.ImageUrl?.Trim() ?? string.Empty,
            Slug = slug,
            IsPublished = model.IsPublished,
            ShowOnHomepage = model.ShowOnHomepage,
            PublishedAt = model.PublishedAt.ToUniversalTime(),
            CreatedAt = DateTime.UtcNow
        };

        db.NewsArticles.Add(article);
        await db.SaveChangesAsync();
        TempData["Success"] = "Haber eklendi.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("haber/duzenle/{id:int}")]
    [Authorize]
    public async Task<IActionResult> EditNews(int id)
    {
        var article = await db.NewsArticles.FindAsync(id);
        if (article is null)
            return NotFound();

        return View("EditNews", new NewsEditViewModel
        {
            Id = article.Id,
            Title = article.Title,
            Summary = article.Summary,
            Content = article.Content,
            Category = article.Category,
            ImageUrl = string.IsNullOrEmpty(article.ImageUrl) ? null : article.ImageUrl,
            IsPublished = article.IsPublished,
            ShowOnHomepage = article.ShowOnHomepage,
            PublishedAt = article.PublishedAt.ToLocalTime()
        });
    }

    [HttpPost("haber/duzenle/{id:int}")]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditNews(int id, NewsEditViewModel model)
    {
        if (model.Id != id)
            return BadRequest();

        var article = await db.NewsArticles.FindAsync(id);
        if (article is null)
            return NotFound();

        if (model.ImageFile is { Length: > 0 })
        {
            var upload = await imageService.SaveAsync(model.ImageFile);
            if (!upload.Success)
            {
                ModelState.AddModelError(nameof(model.ImageFile), upload.Error ?? "Görsel yüklenemedi.");
                return View("EditNews", model);
            }

            imageService.DeleteIfUploaded(article.ImageUrl);
            model.ImageUrl = upload.Path;
        }
        else
        {
            model.ImageUrl = article.ImageUrl;
        }

        if (!ModelState.IsValid)
            return View("EditNews", model);

        article.Title = model.Title.Trim();
        article.Summary = model.Summary.Trim();
        article.Content = model.Content.Trim();
        article.Category = model.Category;
        article.ImageUrl = model.ImageUrl?.Trim() ?? string.Empty;
        article.IsPublished = model.IsPublished;
        article.ShowOnHomepage = model.ShowOnHomepage;
        article.PublishedAt = model.PublishedAt.ToUniversalTime();
        article.UpdatedAt = DateTime.UtcNow;

        var newSlug = SlugHelper.ToSlug(model.Title);
        if (article.Slug != newSlug)
            article.Slug = await EnsureUniqueSlugAsync(newSlug, article.Id);

        await db.SaveChangesAsync();
        TempData["Success"] = "Haber güncellendi.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost("haber/sil/{id:int}")]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteNews(int id)
    {
        var article = await db.NewsArticles.FindAsync(id);
        if (article is null)
            return NotFound();

        imageService.DeleteIfUploaded(article.ImageUrl);
        db.NewsArticles.Remove(article);
        await db.SaveChangesAsync();
        TempData["Success"] = "Haber silindi.";
        return RedirectToAction(nameof(Index));
    }

    private async Task<string> EnsureUniqueSlugAsync(string baseSlug, int? excludeId = null)
    {
        var slug = baseSlug;
        var i = 1;
        while (await db.NewsArticles.AnyAsync(n => n.Slug == slug && n.Id != excludeId))
            slug = $"{baseSlug}-{i++}";
        return slug;
    }
}
