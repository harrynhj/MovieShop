using ApplicationCore.Contracts.Repository;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Azure;
using Infrastructure.Helpers;

namespace Infrastructure.Services;

public class MovieService : IMovieService
{
    
    private readonly IMovieRepository _movieRepository;
    
    public MovieService(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    
    public List<MovieCardModel> GetTop20GrossingMovies()
    {
        var movies =  _movieRepository.GetTop20GrossingMovies();
        var movieCardModels = new List<MovieCardModel>();
        foreach (var movie in movies)
        {
            movieCardModels.Add(new MovieCardModel()
            {
                Id = movie.Id, PosterURL = movie.PosterUrl, Title = movie.Title
            });
        }

        return movieCardModels;
    }

    public PaginatedModel<MovieCardModel> GetMoviesByPage(int pageNumber, int genre = -1)
    {
        double moviesPerPage = 30;
        var movies = _movieRepository.GetMoviesByPage(pageNumber, genre);
        var totalPages = (int)Math.Ceiling(_movieRepository.GetMovieCount(genre)/ moviesPerPage);
        var result = new PaginatedModel<MovieCardModel>();
        
        foreach (var movie in movies)
        {
            result.Items.Add(new MovieCardModel()
            {
                Id = movie.Id, PosterURL = movie.PosterUrl, Title = movie.Title
            });
        }
        result.CurrentIndex = pageNumber;
        result.TotalPages = totalPages;
        result.Genre = genre;
        return result;
    }

    public MovieDetailsModel GetMovieDetails(int id)
    {
        var movie = _movieRepository.GetById(id);
        var genres = GetMovieGenres(id);
        var score = GetMovieRating(id);
        if (movie != null)
        {
            var movieDetailsModel = new MovieDetailsModel()
            {
                Id = movie.Id,
                BackDropUrl =  movie.BackdropUrl,
                ImdbUrl = movie.ImdbUrl,
                OriginalLanguage = movie.OriginalLanguage,
                Price = movie.Price,
                ReleaseDate =  movie.ReleaseDate,
                Runtime = movie.RunTime,
                TmdbUrl = movie.TmdbUrl,
                PosterURL = movie.PosterUrl,
                Title = movie.Title,
                Budget = movie.Budget,
                Overview = movie.Overview,
                TagLine = movie.Tagline,
                Revenue = movie.Revenue,
                Genres = genres,
                Rating = score
            };
            return movieDetailsModel;
        }

        return null;
    }


    public bool DeleteMovie(int id)
    {
        var movie = _movieRepository.DeleteById(id);
        if (movie == null)
        {
            return false;
        }

        return true;
    }

    public Movie GetMoviebyId(int id)
    {
        var movie = _movieRepository.GetById(id);
        if (movie != null)
        {
            return movie;
        }
        return null;
    }

    public List<string> GetMovieGenres(int id)
    {
        List<string> res = new List<string>();
        var genres = _movieRepository.GetMoviesGenres(id);
        foreach (var genre in genres)
        {
            res.Add(GenreMapping.Genres[genre]);
        }
        return res;
    }

    public decimal GetMovieRating(int id)
    {
        List<decimal> ratings = new List<decimal>();
        var reviews =  _movieRepository.GetMovieRating(id);
        if (reviews.Count() == 0)
        {
            return 0;
        }
        foreach (var review in reviews)
        {
            ratings.Add(review.Rating);
        }
        return ratings.Sum() / ratings.Count;
    }
}