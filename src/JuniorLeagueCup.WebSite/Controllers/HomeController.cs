using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using JuniorLeagueCup.WebSite.Models;

namespace JuniorLeagueCup.WebSite.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => View();

    public IActionResult Hakkimizda() => View();

    public IActionResult Turnuvalar() => View();

    public IActionResult KatilimGereklilikleri() => View();

    public IActionResult Iletisim() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
