using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services;


public interface IMovieService
{
    List<MovieCardModel> GetTop20GrossingMovies();
    PaginatedModel<MovieCardModel> GetMoviesByPage(int pageNumber, int genre = -1);
    MovieDetailsModel GetMovieDetails(int id, int userId);
    Movie GetMoviebyId(int id);
    List<string> GetMovieGenres(int id);
    decimal GetMovieRating(int id);
    List<TrailerModel> GetMovieTrailers(int id);
    List<MovieCastModel> GetMovieCasts(int id);
}