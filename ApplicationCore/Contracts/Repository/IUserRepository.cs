using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Repository;

public interface IUserRepository : IRepository<User>
{
    bool CheckEmail(string email);
    User GetUserByEmail(string email);
    string GetRoleById(int id);
    bool InsertReview(Review review);
    bool InsertPurchase(Purchase purchase);
    bool DeleteFavoriteMovie(int movieId, int userId);
    bool AddFavoriteMovie(int movieId, int userId);
    IEnumerable<Movie> GetFavMoviesByPage(int pageNumber, int userId);
    int GetFavMovieCount(int userId);
    IEnumerable<Purchase> GetPurchasedMoviesByPage(int pageNumber, int userId);
    int GetPurchasedMoviesCount(int userId);
    
    
}