using ApplicationCore.Contracts.Repository;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(MovieShopDbContext dbContext): base(dbContext)
    {
        
    }

    public bool CheckEmail(string email)
    {
        return _movieShopDbContext.Users.Any(u => u.Email == email);
    }

    public User GetUserByEmail(string email)
    {
        return _movieShopDbContext.Users.Where(u => u.Email == email).FirstOrDefault();
    }

    public string GetRoleById(int id)
    {
        var userTask = _movieShopDbContext.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Id == id);

        var user = userTask.Result;

        if (user != null)
        {
            var adminRole = user.Role.FirstOrDefault(r => r.Name == "Admin");
            if (adminRole != null)
            {
                return "Admin";
            }
            else
            {
                return "User";
            }
        }
        return null;
    }

    public bool InsertReview(Review review)
    {
        var dup = _movieShopDbContext.Reviews.Any(r => r.MovieId == review.MovieId && r.UserId == review.UserId);
        if (dup)
        {
            _movieShopDbContext.Entry(review).State = EntityState.Modified;
            _movieShopDbContext.SaveChanges();
            return true;
        }

        _movieShopDbContext.Set<Review>().Add(review);
        _movieShopDbContext.SaveChanges();
        return true;
    }

    public bool InsertPurchase(Purchase purchase)
    {
        _movieShopDbContext.Set<Purchase>().Add(purchase);
        _movieShopDbContext.SaveChanges();
        return true;
    }

    public bool DeleteFavoriteMovie(int movieId, int userId)
    {
        var user = _movieShopDbContext.Users
            .Include(u => u.Movie)
            .FirstOrDefault(u => u.Id == userId);
        if (user == null) return false;
        var movie = user.Movie.FirstOrDefault(m => m.Id == movieId);
        if (movie == null) return false;
        user.Movie.Remove(movie);
        _movieShopDbContext.SaveChanges();
        return true;
    }

    public bool AddFavoriteMovie(int movieId, int userId)
    {
        var user = _movieShopDbContext.Users
            .Include(u => u.Movie)
            .FirstOrDefault(u => u.Id == userId);
        if (user == null) return false;
        var movie = _movieShopDbContext.Movies.Find(movieId);
        if (movie == null) return false;
        if (user.Movie.Any(m => m.Id == movieId))
            return true;
        user.Movie.Add(movie);
        _movieShopDbContext.SaveChanges();
        return true;
    }

    public IEnumerable<Movie> GetFavMoviesByPage(int pageNumber, int userId)
    {
        int pageSize = 30;
        var user = _movieShopDbContext.Users
            .Include(u => u.Movie)
            .FirstOrDefault(u => u.Id == userId);
        if (user == null || user.Movie == null)
        {
            return Enumerable.Empty<Movie>();
        }
        return user.Movie
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }

    public int GetFavMovieCount(int userId)
    {
        var user = _movieShopDbContext.Users
            .Include(u => u.Movie)
            .FirstOrDefault(u => u.Id == userId);

        if (user == null)
        {
            return -1;
        }
        return user.Movie?.Count ?? 0;
    }

    public IEnumerable<Purchase> GetPurchasedMoviesByPage(int pageNumber, int userId)
    {
        int pageSize = 30;
        var movies = _movieShopDbContext.Purchases
            .Where(p => p.UserId == userId)
            .Include(p => p.Movie)
            .OrderByDescending(p => p.PurchaseDateTime)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();
        return movies;
    }

    public int GetPurchasedMoviesCount(int userId)
    {
        return _movieShopDbContext.Purchases
            .Count(p => p.UserId == userId);
    }
}