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

    public MovieDetailsModel GetMovieDetails(int id, int userId)
    {
        var movie = _movieRepository.GetById(id);
        var genres = GetMovieGenres(id);
        var score = GetMovieRating(id);
        var trailers = GetMovieTrailers(id);
        var cast = GetMovieCasts(id);
        var isOwned = false;
        var isFavorite = false;
        if (userId != -1)
        {
            isOwned = _movieRepository.GetPurchaseStatus(id,userId);
            isFavorite = _movieRepository.GetFavoriteStatus(id, userId);
        }
        
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
                Rating = score,
                Trailer = trailers,
                Cast = cast,
                isOwned = isOwned,
                isFavorite = isFavorite
            };
            return movieDetailsModel;
        }

        return null;
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
            return -1;
        }
        foreach (var review in reviews)
        {
            ratings.Add(review.Rating);
        }
        return ratings.Sum() / ratings.Count;
    }

    public List<TrailerModel> GetMovieTrailers(int id)
    {
        List<TrailerModel> res = new List<TrailerModel>();
        var trailers = _movieRepository.GetMovieTrailers(id);
        foreach (var trailer in trailers)
        {
            res.Add(new TrailerModel()
            {
                Title = trailer.Name,
                Url = trailer.TrailerUrl
            });
        }
        return res;
    }

    public List<MovieCastModel> GetMovieCasts(int id)
    {
        List<MovieCastModel> res = new List<MovieCastModel>();
        var casts = _movieRepository.GetMovieCasts(id);
        foreach (var cast in casts)
        {
            res.Add(new MovieCastModel()
            {
                CastId = cast.CastId,
                CastName = cast.Cast.Name,
                CharacterName = cast.Character,
                AvatarUrl = cast.Cast.ProfilePath,
                TmdbUrl = cast.Cast.TmdbUrl
            });
        }
        return res;
    }
}