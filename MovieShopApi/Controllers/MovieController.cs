using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopAPI.Controllers;

public class MovieController : ControllerBase
{
    
    private readonly IMovieService _movieService;
    private readonly IAdminService _adminService;
    
    public MovieController(IMovieService movieService, IAdminService adminService)
    {
        _movieService = movieService;
        _adminService = adminService;
        
    }
    
    [HttpGet("details/{id}")]
    [ProducesResponseType(typeof(MovieDetailsModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetMovieDetail(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Movie ID must be greater than 0.");
        }

        var movie = await _movieService.GetMovieDetails(id, -1);

        if (movie == null)
        {
            return NotFound($"No movie found with ID {id}.");
        }

        return Ok(movie);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateMovie([FromBody] NewMovieModel movie)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        int movieId = await _adminService.InsertMovie(movie, "API");

        if (movieId != -1)
        {
            return CreatedAtAction(nameof(GetMovieDetail), new { id = movieId }, new { id = movieId });
        }

        return StatusCode(500, "Failed to add movie. Please try again.");
    }
    
    [HttpGet("movies")]
    [ProducesResponseType(typeof(PaginatedModel<MovieCardModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetPagedMovies([FromQuery] int page = 1, [FromQuery] int genre = -1)
    {
        if (page < 1)
        {
            return BadRequest("Page number must be greater than 0.");
        }

        var movies = await _movieService.GetMoviesByPage(page, genre);
        return Ok(movies);
    }
    
}