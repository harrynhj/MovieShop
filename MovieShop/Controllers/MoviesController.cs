using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace MovieShop.Controllers;

public class MoviesController : Controller
{
    private readonly IMovieService _movieService;
    public MoviesController(IMovieService movieService)
    {
        _movieService = movieService;
    }
        
    // GET: MoviesController
    public ActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult MovieDetails(int id)
    {
        var movie = _movieService.GetMovieDetails(id);
        return View(movie);
    }

    [HttpPost]
    public IActionResult DeleteMovie(int id)
    {
        var movie = _movieService.GetMovieDetails(id);
        if (movie == null)
        {
            return NotFound();
        }

        _movieService.DeleteMovie(id);
        return RedirectToAction("Index","Home");
    }
}