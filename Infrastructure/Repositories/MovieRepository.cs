using ApplicationCore.Contracts.Repository;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class MovieRepository : Repository<Movie>, IMovieRepository
{
    public MovieRepository(MovieShopDbContext dbContext): base(dbContext) { }

    public Movie GetHighestGrossingMovies()
    {
        var movie = _movieShopDbContext.Movies.OrderByDescending(movie => movie.Revenue).ToList().First();
        return movie;
    }

    public IEnumerable<Movie> GetTop20GrossingMovies()
    {
        var movies = _movieShopDbContext.Movies.OrderByDescending(m => m.Revenue).Take(20);
        return movies;
    }

    public IEnumerable<Movie> GetMoviesByPage(int pageNumber, int genre = -1)
    {
        int pageSize = 30;
        var query = _movieShopDbContext.Movies.AsQueryable();
        if (genre != -1)
        {
            query = query
                .Join(_movieShopDbContext.MovieGenres,
                    movie => movie.Id,
                    movieGenre => movieGenre.MovieId, // 修正这里
                    (movie, movieGenre) => new { movie, movieGenre })
                .Where(mg => mg.movieGenre.GenreId == genre)
                .Select(mg => mg.movie)
                .Distinct();
        }
        var movies = query
            .OrderByDescending(m => m.Revenue)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();
        return movies;
    }

    public int GetMovieCount(int genre = -1)
    {
        if (genre == -1)
        {
            return _movieShopDbContext.Movies.Count();
        }
        return _movieShopDbContext.MovieGenres
            .Where(mg => mg.GenreId == genre)
            .Select(mg => mg.MovieId)
            .Distinct()
            .Count();
    }

    public IEnumerable<int> GetMoviesGenres(int movie)
    {
        var genres = _movieShopDbContext
            .MovieGenres
            .Where(g => g.MovieId == movie)
            .Select(g => g.GenreId)
            .Distinct()
            .ToList();
        return genres;
    }

    public IEnumerable<Review> GetMovieRating(int id)
    {
        var reviews = _movieShopDbContext
            .Reviews
            .Where(r => r.MovieId == id)
            .ToList();
        return reviews;
    }
}