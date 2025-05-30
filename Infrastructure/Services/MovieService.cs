using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;

namespace Infrastructure.Services;

public class MovieService : IMovieService
{
    public List<MovieCardModel> Top30GrossingMovies()
    {
        var movies = new List<MovieCardModel>()
        {
            new MovieCardModel()
            {
                Id = 1, Title = "Fast", PosterURL = "https://image.tmdb.org/t/p/w500/1E5baAaEse26fej7uHcjOgEE2t2.jpg"
            }

        };
        return movies;
    }
}