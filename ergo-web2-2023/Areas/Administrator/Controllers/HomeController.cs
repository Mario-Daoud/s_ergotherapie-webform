using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ergo_web2_2023.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Localization;

namespace ergo_web2_2023.Areas.Administrator.Controllers;

[Authorize]
[Area("Administrator")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;


    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
        
    }

    public IActionResult Index()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}