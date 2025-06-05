using System.Security.Claims;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MovieShop.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly IAdminService _adminService;
    
    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }
    
    public async Task<IActionResult> TopMovies(DateTime? start = null, DateTime? end = null, int page = 1)
    {
        var report = await _adminService.GetSellReport(start, end, page);
        return View(report);
    }
    
    public IActionResult AddMovie()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> AddMovie(NewMovieModel model)
    {
        string fullName = User.FindFirstValue(ClaimTypes.Name);
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        int movieId = await _adminService.InsertMovie(model, fullName);
        if (movieId != -1)
        {
            return RedirectToAction("MovieDetails", "Movies", new { id = movieId });
        }
        ModelState.AddModelError(string.Empty, "Add movie failed. Please try again.");
        return View(model);
    }
    
    [HttpPost]
    public async Task<IActionResult> DeleteMovie(int id)
    {
        await _adminService.DeleteMovie(id);
        return RedirectToAction("Index","Home");
    }

    [HttpPost]
    public async Task<IActionResult> EditPrice(MoviePriceModel model)
    {
        ModelState.Remove("Review");
        ModelState.Remove("Movie");
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        await _adminService.UpdatePrice(model);
        return RedirectToAction("MovieDetails", "Movies", new { id = model.MovieId });
    }
}