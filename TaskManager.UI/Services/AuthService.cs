using TaskManager.UI.Infrastructure.Auth;
using TaskManager.UI.Infrastructure.Shared.Contracts;
using TaskManager.UI.Infrastructure.User;

namespace TaskManager.UI.Services;

public class AuthService : IAuthService
{
    private readonly CustomAuthStateProvider _authProvider;
    private readonly IUserService _userService;
    
    public AuthService(IUserService userService, CustomAuthStateProvider authProvider)
    {
        _userService = userService;
        _authProvider = authProvider;
    }
    
    public async Task<bool> RegisterAsync(RegisterRequest request)
    {
        int result = await _userService.Register(request);
        return result != -1;
    }

    public async Task<bool> LoginAsync(LoginRequest requestBody)
    {
        bool result = await _userService.Login(requestBody);
        if (!result)
        {
            return false;
        }
        
        return await _authProvider.NotifyUserLoggedIn();
    }
    
    public async Task<bool> IsAuthenticatedAsync()
    {
        var authState = await _authProvider.GetAuthenticationStateAsync();
        var user = authState.User;
        return user.Identity?.IsAuthenticated ?? false;
    }

    public async Task<bool> LogoutAsync()
    {
        var result = await _userService.Logout();
        if (!result)
        {
            return false;
        }
        
        _authProvider.NotifyUserLoggedOut();
        return true;
    }
}