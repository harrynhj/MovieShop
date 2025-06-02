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
        var user = _movieShopDbContext.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user != null)
        {
            return user.Result.Role.FirstOrDefault(r => r.Name == "Admin").Name;
        }
        return null;
    }
}