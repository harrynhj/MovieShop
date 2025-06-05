using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services;

public interface IUserService
{
    Task<bool> AddReview(ReviewModel model);
    Task<bool> PurchaseMovie(int movieId, int userId, decimal price);
    Task<bool> ToggleFavoriteMovie(int movieId, int userId);
    Task<PaginatedModel<MovieCardModel>> GetUserFavMovies(int pageNumber, int userId);
    Task<PaginatedModel<PurchasedMovieCardModel>> GetUserPurchasedMovies(int pageNumber, int userId);
}