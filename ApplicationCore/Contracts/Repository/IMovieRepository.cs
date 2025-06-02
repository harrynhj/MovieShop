using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Repository;

public interface IMovieRepository: IRepository<Movie>
{
    Movie GetHighestGrossingMovies();
    IEnumerable<Movie> GetTop20GrossingMovies();
    IEnumerable<Movie> GetMoviesByPage(int pageNumber, int genre = -1);
    int GetMovieCount(int genre = -1);
    IEnumerable<int> GetMoviesGenres(int genre);
    IEnumerable<Review> GetMovieRating(int id);
    IEnumerable<Trailer> GetMovieTrailers(int id);
    IEnumerable<MovieCast> GetMovieCasts(int id);
}