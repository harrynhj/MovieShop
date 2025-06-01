using System.Diagnostics;
using ApplicationCore.Contracts.Services;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using MovieShop.Models;

namespace MovieShop.Controllers;

public class HomeController : Controller
{
    private readonly IMovieService _movieService;
    
    public HomeController(IMovieService movieService)
    {
        _movieService = movieService;
    }
    

    public IActionResult Index(int page = 1, int genre = -1)
    {
        // var movies= _movieService.GetTop20GrossingMovies();
        var movies = _movieService.GetMoviesByPage(page, genre);
        return View(movies);
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}