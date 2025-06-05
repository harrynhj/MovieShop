using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Repository;

public interface IMovieRepository: IRepository<Movie>
{
    Task<Movie> GetHighestGrossingMovies();
    Task<IEnumerable<Movie> >GetTop20GrossingMovies();
    Task<IEnumerable<Movie>> GetMoviesByPage(int pageNumber, int genre = -1, int pageSize = 30);
    Task<int> GetMovieCount(int genre = -1);
    Task<IEnumerable<int>> GetMoviesGenres(int genre);
    Task<IEnumerable<Review>>GetMovieRating(int id);
    Task<IEnumerable<Trailer>> GetMovieTrailers(int id);
    Task<IEnumerable<MovieCast>> GetMovieCasts(int id);
    Task<bool> GetPurchaseStatus(int id, int userId);
    Task<bool> GetFavoriteStatus(int id, int userId);
}