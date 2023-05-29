using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ergo_web2_2023.Models;
using ergo_web2_2023.Services;
using ergo_web2_2023.Models.Entities;
using Microsoft.Extensions.Localization;
using ergo_web2_2023.ViewModels;
using System.Collections.Generic;
using AutoMapper;
using ergo_web2_2023.Services.Interfaces;
using ergo_web2_2023.Areas.Administrator.ViewModels;

namespace ergo_web2_2023.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private IBasicOperationService<Form> _formService;
    private readonly IMapper _mapper;

    public HomeController(ILogger<HomeController> logger, IMapper mapper, IBasicOperationService<Form> formService)
    {
        _logger = logger;
        _formService = formService;
        _mapper = mapper;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public async Task<IActionResult> Forms()
    {
        var list = await _formService.GetAll();
        List<Areas.Administrator.ViewModels.FormVM> listVM = _mapper.Map<List<Areas.Administrator.ViewModels.FormVM>>(list);
        return View(listVM);
    }
    
    public IActionResult Redirect()
    {
        return RedirectToAction("Index", "Question");
    }

}