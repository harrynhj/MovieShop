using System.Diagnostics;
using System.Security.Claims;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieShop.Models;

namespace MovieShop.Controllers;

[Authorize]
public class UserController : Controller
{
    private readonly IUserService _userService;
    public UserController(IUserService userService, IMovieService movieService)
    {
        _userService = userService;
    }
    
    public IActionResult Profile()
    {
        return View();
    }
    [Authorize]
    public IActionResult Favorite(int page = 1)
    {
        var userId = User.Identity.IsAuthenticated ? Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)) : -1;
        var movies = _userService.GetUserFavMovies(page,userId);
        return View(movies);
    }
    
    public IActionResult Purchase(int page = 1)
    {
        var userId = User.Identity.IsAuthenticated ? Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)) : -1;
        var movies = _userService.GetUserPurchasedMovies(page,userId);
        return View(movies);
    }
    
    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult SubmitReview(MovieDetailPageModel model)
    {
        ModelState.Remove("Movie");
        ModelState.Remove("Price");
        _userService.AddReview(model.Review);
        return RedirectToAction("MovieDetails", "Movies", new { id = model.Review.MovieId });
    }
    
    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Purchase(int movieId,decimal price)
    {
        var userId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        
        _userService.PurchaseMovie(movieId, userId, price);
        return RedirectToAction("MovieDetails", "Movies", new { id = movieId });
    }
    
    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ToggleFavorite(int movieId)
    {
        var userId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        _userService.ToggleFavoriteMovie(movieId, userId);
        return RedirectToAction("MovieDetails", "Movies", new { id = movieId });
    }
    
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}