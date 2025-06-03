using ApplicationCore.Contracts.Repository;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Infrastructure.Helpers;

namespace Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    
    public UserService(IUserRepository userRepository,  IMovieRepository movieRepository)
    {
        _userRepository = userRepository;
    }


    public bool AddReview(ReviewModel model)
    {
        var reviews = new Review()
        {
            MovieId = model.MovieId,
            UserId = model.UserId,
            CreatedDate = DateTime.Now,
            Rating = model.Score,
            ReviewText = model.Review
        };
        _userRepository.InsertReview(reviews);
        return true;
    }
    
    public bool PurchaseMovie(int movieId, int userId, decimal price)
    {
        var purchase = new Purchase()
        {
            MovieId = movieId,
            UserId = userId,
            PurchaseDateTime = DateTime.Now,
            PurchaseNumber = Guid.NewGuid(),
            TotalPrice = price,


        };
        _userRepository.InsertPurchase(purchase);
        return true;
    }

    public bool ToggleFavoriteMovie(int movieId, int userId)
    {
        if (_userRepository.DeleteFavoriteMovie(movieId, userId))
        {
            return true;
        }
        _userRepository.AddFavoriteMovie(movieId, userId);
        return true;
    }

    public PaginatedModel<MovieCardModel> GetUserFavMovies(int pageNumber, int userId)
    {
        double moviesPerPage = 30;
        var movies = _userRepository.GetFavMoviesByPage(pageNumber, userId);
        var totalPages = (int)Math.Ceiling(_userRepository.GetFavMovieCount(userId)/ moviesPerPage);
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
        return result;
    }

    public PaginatedModel<PurchasedMovieCardModel> GetUserPurchasedMovies(int pageNumber, int userId)
    {
        double moviesPerPage = 30;
        var movies = _userRepository.GetPurchasedMoviesByPage(pageNumber, userId);
        var totalPages = (int)Math.Ceiling(_userRepository.GetPurchasedMoviesCount(userId)/ moviesPerPage);
        var result = new PaginatedModel<PurchasedMovieCardModel>();
        
        foreach (var movie in movies)
        {
            result.Items.Add(new PurchasedMovieCardModel()
            {
                Id = movie.Movie.Id, 
                PosterURL = movie.Movie.PosterUrl, 
                Title = movie.Movie.Title,
                PurchaseDate = movie.PurchaseDateTime,
                Price = movie.TotalPrice,
                PurchasedNumber = movie.PurchaseNumber,
            });
        }
        result.CurrentIndex = pageNumber;
        result.TotalPages = totalPages;
        return result;
    }
}