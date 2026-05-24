using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace JuniorLeagueCup.WebSite.Controllers;

public class CultureController : Controller
{
    [HttpGet("culture/set")]
    public IActionResult Set(string culture, string? returnUrl = null)
    {
        culture = culture switch
        {
            "en" or "en-US" or "en-GB" => "en",
            "es" or "es-ES" => "es",
            "de" or "de-DE" => "de",
            _ => "tr-TR"
        };

        Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
            new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddYears(1),
                IsEssential = true,
                SameSite = SameSiteMode.Lax
            });

        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            return LocalRedirect(returnUrl);

        return RedirectToAction("Index", "Home");
    }
}
