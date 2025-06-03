using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services;

public interface IAccountService
{
    bool RegisterAccount(RegisterModel model);
    UserModel AuthenticateUser(LoginModel model);
    bool HasDupEmail(string email);
}