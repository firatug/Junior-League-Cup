using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using JuniorLeagueCup.WebSite.Models;

namespace JuniorLeagueCup.WebSite.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => View();

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
    public IActionResult TakimBasvurusu() => View();

    [Route("takim-basvurusu")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult TakimBasvurusuPost()
    {
        ViewBag.FormSuccess = true;
        return View("TakimBasvurusu");
    }

    [Route("veli-oyuncu-deneyimi")]
    public IActionResult VeliOyuncuDeneyimi() => View();

    [Route("scout-sistemi")]
    public IActionResult ScoutSistemi() => View();

    [Route("sponsorluk")]
    [HttpGet]
    public IActionResult Sponsorluk() => View();

    [Route("sponsorluk")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult SponsorlukPost()
    {
        ViewBag.FormSuccess = true;
        return View("Sponsorluk");
    }

    [Route("medya")]
    public IActionResult Medya() => View();

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
    public IActionResult Iletisim() => View();

    [Route("iletisim")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult IletisimPost()
    {
        ViewBag.FormSuccess = true;
        return View("Iletisim");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
