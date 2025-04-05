using TaskManager.UI.Infrastructure.Shared.Contracts;

namespace TaskManager.UI.Infrastructure.Auth;

public interface IAuthService
{
    Task<bool> RegisterAsync(RegisterRequest request);
    Task<bool> LoginAsync(LoginRequest requestBody);
    Task<bool> IsAuthenticatedAsync();
    Task<bool> LogoutAsync();
}