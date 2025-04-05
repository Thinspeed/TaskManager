using TaskManager.UI.Infrastructure.Shared.Contracts;

namespace TaskManager.UI.Infrastructure.User;

public interface IUserService
{
    Task<int> Register(RegisterRequest request);
    Task<bool> Login(LoginRequest request);
    Task<bool> Logout();
    Task<CurrentUserResponse?> GetCurrentUser();
    Task<GetUserResponse?> GetUserById(int id);
}