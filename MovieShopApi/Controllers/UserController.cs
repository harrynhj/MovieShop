using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopAPI.Controllers;

public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    
    public UserController(IUserService userService, IMovieService movieService)
    {
        _userService = userService;
    }
    
    [HttpGet("favorites")]
    [ProducesResponseType(typeof(List<MovieCardModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetFavorites([FromQuery] int userId, [FromQuery] int page = 1)
    {
        if (userId <= 0 || page < 1)
        {
            return BadRequest("Invalid user ID or page number.");
        }

        var movies = await _userService.GetUserFavMovies(page, userId);
        return Ok(movies);
    }
    
    [HttpGet("purchases")]
    [ProducesResponseType(typeof(List<MovieCardModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetPurchases([FromQuery] int userId, [FromQuery] int page = 1)
    {
        if (userId <= 0 || page < 1)
        {
            return BadRequest("Invalid user ID or page number.");
        }

        var movies = await _userService.GetUserPurchasedMovies(page, userId);
        return Ok(movies);
    }
    
    [HttpPost("review")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SubmitReview([FromBody] ReviewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _userService.AddReview(model);

        return Ok(new { message = "Review submitted successfully." });
    }
    
    [HttpPost("purchase")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PurchaseMovie([FromBody] PurchasesAPI model)
    {
        if (model.UserId <= 0 || model.MovieId <= 0 || model.Price <= 0)
        {
            return BadRequest("Invalid purchase data.");
        }

        await _userService.PurchaseMovie(model.MovieId, model.UserId, model.Price);
        return Ok(new { message = "Movie purchased successfully." });
    }
}