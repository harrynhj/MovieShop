using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Repository;

public interface IUserRepository : IRepository<User>
{
    bool CheckEmail(string email);
    User GetUserByEmail(string email);
    string GetRoleById(int id);
}