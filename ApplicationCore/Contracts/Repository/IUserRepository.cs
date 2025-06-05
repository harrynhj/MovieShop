using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Repository;

public interface IUserRepository : IRepository<User>
{
    Task<bool> CheckEmail(string email);
    Task<User> GetUserByEmail(string email);
    Task<string> GetRoleById(int id);
    Task<bool> InsertReview(Review review);
    Task<bool> InsertPurchase(Purchase purchase);
    Task<bool> DeleteFavoriteMovie(int movieId, int userId);
    Task<bool> AddFavoriteMovie(int movieId, int userId);
    Task<IEnumerable<Movie>> GetFavMoviesByPage(int pageNumber, int userId, int pageSize = 30);
    Task<int> GetFavMovieCount(int userId);
    Task<IEnumerable<Purchase>> GetPurchasedMoviesByPage(int pageNumber, int userId, int pageSize = 30);
    Task<int> GetPurchasedMoviesCount(int userId);
    
    
}