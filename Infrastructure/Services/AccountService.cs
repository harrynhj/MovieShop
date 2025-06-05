using ApplicationCore.Contracts.Repository;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Infrastructure.Helpers;

namespace Infrastructure.Services;

public class AccountService : IAccountService
{
    
    private readonly IUserRepository _userRepository;
    
    public AccountService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    
    public async Task<bool> RegisterAccount(RegisterModel model)
    {
        string salt = PasswordHelper.GenerateSalt();
        string hashedPassword = PasswordHelper.HashPassword(model.Password, salt);
        var user = new User()
        {
            DateOfBirth = model.DateOfBirth,
            Email = model.Email,
            FirstName = model.FirstName,
            HashedPassword = hashedPassword,
            IsLocked = false,
            LastName = model.LastName,
            Salt = salt
        };
        await _userRepository.Insert(user);
        return true;
    }

    public async Task<UserModel> AuthenticateUser(LoginModel model)
    {
        var user = await _userRepository.GetUserByEmail(model.Email);
        if (user == null) return null;
        var hashedInput = PasswordHelper.HashPassword(model.Password, user.Salt);
        if (user.HashedPassword == hashedInput)
        {
            string? role = await _userRepository.GetRoleById(user.Id);
            if (role == null)
            {
                role = "User";
            }
            return new UserModel()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = user.Id,
                role = role
            };
        }
        return null;
    }
    

    public async Task<bool> HasDupEmail(string email)
    {
        if (await _userRepository.CheckEmail(email))
        {
            return true;
        }
        return false;
    }
}