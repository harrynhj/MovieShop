using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services;

public interface IUserService
{
    bool RegisterAccount(RegisterModel model);
    UserModel AuthenticateUser(LoginModel model);
    bool HasDupEmail(string email);
    
}