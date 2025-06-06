using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services;


public interface IMovieService
{
    Task<List<MovieCardModel>> GetTop20GrossingMovies();
    Task<PaginatedModel<MovieCardModel>> GetMoviesByPage(int pageNumber, int genre = -1);
    Task<MovieDetailsModel> GetMovieDetails(int id, int userId = -1);
    Task<Movie> GetMoviebyId(int id);
    Task<List<string>> GetMovieGenres(int id);
    Task<decimal> GetMovieRating(int id);
    Task<List<TrailerModel>> GetMovieTrailers(int id);
    Task<List<MovieCastModel>> GetMovieCasts(int id);
}