using System.Diagnostics;
using ApplicationCore.Contracts.Services;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using MovieShop.Models;

namespace MovieShop.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IMovieService _movieService;
    
    public HomeController(ILogger<HomeController> logger, IMovieService movieService)
    {
        _logger = logger;
        _movieService = movieService;
    }
    

    public IActionResult Index()
    {
        var movieService = new MovieService();
        
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
}