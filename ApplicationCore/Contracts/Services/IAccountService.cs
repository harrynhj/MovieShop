using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services;

public interface IAccountService
{
    Task<bool> RegisterAccount(RegisterModel model);
    Task<UserModel> AuthenticateUser(LoginModel model);
    Task<bool> HasDupEmail(string email);
}