using ApplicationCore.Contracts.Repository;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class MovieRepository : Repository<Movie>, IMovieRepository
{
    public MovieRepository(MovieShopDbContext dbContext): base(dbContext) { }

    public async Task<Movie> GetHighestGrossingMovies()
    {
        var movie = await _movieShopDbContext.Movies.OrderByDescending(movie => movie.Revenue).ToListAsync();
        var res = movie.First();
        return res;
    }

    public async Task<IEnumerable<Movie>> GetTop20GrossingMovies()
    {
        var movies = await _movieShopDbContext.Movies.OrderByDescending(m => m.Revenue).Take(20).ToListAsync();
        return movies;
    }

    public async Task<IEnumerable<Movie>> GetMoviesByPage(int pageNumber, int genre = -1, int pageSize = 30)
    {
        var query = _movieShopDbContext.Movies.AsQueryable();
        if (genre != -1)
        {
            query = query
                .Join(_movieShopDbContext.MovieGenres,
                    movie => movie.Id,
                    movieGenre => movieGenre.MovieId,
                    (movie, movieGenre) => new { movie, movieGenre })
                .Where(mg => mg.movieGenre.GenreId == genre)
                .Select(mg => mg.movie)
                .Distinct();
        }
        var movies = await query
            .OrderByDescending(m => m.Revenue)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return movies;
    }

    public async Task<int> GetMovieCount(int genre = -1)
    {
        if (genre == -1)
        {
            return _movieShopDbContext.Movies.Count();
        }
        return await _movieShopDbContext.MovieGenres
            .Where(mg => mg.GenreId == genre)
            .Select(mg => mg.MovieId)
            .Distinct()
            .CountAsync();
    }

    public async Task<IEnumerable<int>> GetMoviesGenres(int movie)
    {
        var genres = await _movieShopDbContext
            .MovieGenres
            .Where(g => g.MovieId == movie)
            .Select(g => g.GenreId)
            .Distinct()
            .ToListAsync();
        return genres;
    }

    public async Task<IEnumerable<Review>> GetMovieRating(int id)
    {
        var reviews = await _movieShopDbContext
            .Reviews
            .Where(r => r.MovieId == id)
            .ToListAsync();
        return reviews;
    }

    public async Task<IEnumerable<Trailer>> GetMovieTrailers(int id)
    {
        var trailers = await _movieShopDbContext.Trailers
            .Where(t => t.MovieId == id)
            .Distinct()
            .ToListAsync();
        return trailers;
    }

    public async Task<IEnumerable<MovieCast>> GetMovieCasts(int movieId)
    {
        var casts = await _movieShopDbContext.MovieCasts
            .Include(mc => mc.Cast)
            .Where(mc => mc.MovieId == movieId)
            .ToListAsync();

        return casts;
    }

    public async Task<bool> GetPurchaseStatus(int id, int userId)
    {
        return await _movieShopDbContext.Purchases.AnyAsync(p => p.MovieId == id && p.UserId == userId);
    }

    public async Task<bool> GetFavoriteStatus(int id, int userId)
    {
        return await _movieShopDbContext.Users
            .Where(u => u.Id == userId)
            .AnyAsync(u => u.Movie.Any(m => m.Id == id));
    }
    
}