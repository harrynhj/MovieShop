using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services;


public interface IMovieService
{
    List<MovieCardModel> Top30GrossingMovies();
}