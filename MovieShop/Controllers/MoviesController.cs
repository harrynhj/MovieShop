using System.Security.Claims;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace MovieShop.Controllers;

public class MoviesController : Controller
{
    private readonly IMovieService _movieService;
    public MoviesController(IMovieService movieService)
    {
        _movieService = movieService;
        
    }
        

    public ActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult MovieDetails(int id)
    {
        var userId = User.Identity.IsAuthenticated ? Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)) : -1;
        var movie = _movieService.GetMovieDetails(id, userId);
        if (movie == null)
        {
            return RedirectToAction("Index", "Home");
        }
        var review = new ReviewModel()
        {
            MovieId = movie.Id,
            UserId = userId
        };
        var viewModel = new MovieDetailPageModel()
        {
            Movie = movie,
            Review = review
        };
        return View(viewModel);
    }


}