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

    public async Task<bool> CheckEmail(string email)
    {
        return await _movieShopDbContext.Users.AnyAsync(u => u.Email == email);
    }

    public async Task<User> GetUserByEmail(string email)
    {
        return await _movieShopDbContext.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
    }

    public async Task<string> GetRoleById(int id)
    {
        var user = await _movieShopDbContext.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user != null)
        {
            var adminRole = user.Role.FirstOrDefault(r => r.Name == "Admin");
            return adminRole != null ? "Admin" : "User";
        }

        return null;
    }

    public async Task<bool> InsertReview(Review review)
    {
        var dup = await _movieShopDbContext.Reviews.AnyAsync(r => r.MovieId == review.MovieId && r.UserId == review.UserId);
        if (dup)
        {
            _movieShopDbContext.Entry(review).State = EntityState.Modified;
            _movieShopDbContext.SaveChanges();
            return true;
        }

        _movieShopDbContext.Set<Review>().Add(review);
        await _movieShopDbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> InsertPurchase(Purchase purchase)
    {
        _movieShopDbContext.Set<Purchase>().Add(purchase);
        await _movieShopDbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteFavoriteMovie(int movieId, int userId)
    {
        var user = await _movieShopDbContext.Users
            .Include(u => u.Movie)
            .FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null) return false;
        var movie = user.Movie.FirstOrDefault(m => m.Id == movieId);
        if (movie == null) return false;
        user.Movie.Remove(movie);
        await _movieShopDbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> AddFavoriteMovie(int movieId, int userId)
    {
        var user = await _movieShopDbContext.Users
            .Include(u => u.Movie)
            .FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null) return false;
        var movie = _movieShopDbContext.Movies.Find(movieId);
        if (movie == null) return false;
        if (user.Movie.Any(m => m.Id == movieId))
            return true;
        user.Movie.Add(movie);
        await _movieShopDbContext.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Movie>> GetFavMoviesByPage(int pageNumber, int userId, int pageSize = 30)
    {
        var user = await _movieShopDbContext.Users
            .Include(u => u.Movie)
            .FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null || user.Movie == null)
        {
            return Enumerable.Empty<Movie>();
        }
        return user.Movie
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }

    public async Task<int> GetFavMovieCount(int userId)
    {
        var user = await _movieShopDbContext.Users
            .Include(u => u.Movie)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
        {
            return -1;
        }
        return user.Movie?.Count ?? 0;
    }

    public async Task<IEnumerable<Purchase>> GetPurchasedMoviesByPage(int pageNumber, int userId, int pageSize = 30)
    {
        var movies = await _movieShopDbContext.Purchases
            .Where(p => p.UserId == userId)
            .Include(p => p.Movie)
            .OrderByDescending(p => p.PurchaseDateTime)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return movies;
    }

    public async Task<int> GetPurchasedMoviesCount(int userId)
    {
        return await _movieShopDbContext.Purchases
            .CountAsync(p => p.UserId == userId);
    }
}