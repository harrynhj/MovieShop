using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopAPI.Controllers;

public class MovieController : ControllerBase
{
    
    private readonly IMovieService _movieService;
    public MovieController(IMovieService movieService)
    {
        _movieService = movieService;
        
    }
    
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetMovieDetail(int id)
    {
        var movie = await _movieService.GetMovieDetails(id, -1);

        return Ok(movie);
    }

    [HttpPost]
    public IActionResult CreateProduct(int id)
    {

        return Ok("Product created");
    }
}