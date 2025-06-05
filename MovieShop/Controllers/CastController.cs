using System.Diagnostics;
using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Mvc;
using MovieShop.Models;

namespace MovieShop.Controllers;

public class CastController : Controller
{
    private readonly ICastService _castService;
    
    public CastController(ICastService castService)
    {
        _castService = castService;
    }
    

    public async Task<IActionResult> Details(int id)
    {
        var cast = await _castService.GetCastDetails(id);
        return View(cast);
    }



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}