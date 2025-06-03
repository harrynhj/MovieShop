using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services;

public interface IUserService
{
    bool AddReview(ReviewModel model);
    bool PurchaseMovie(int movieId, int userId, decimal price);
    bool ToggleFavoriteMovie(int movieId, int userId);
    PaginatedModel<MovieCardModel> GetUserFavMovies(int pageNumber, int userId);
    PaginatedModel<PurchasedMovieCardModel> GetUserPurchasedMovies(int pageNumber, int userId);
}