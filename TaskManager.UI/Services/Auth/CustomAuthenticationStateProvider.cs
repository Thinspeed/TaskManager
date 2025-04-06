using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using TaskManager.UI.Infrastructure.Shared.Contracts;
using TaskManager.UI.Infrastructure.User;

namespace TaskManager.UI.Services.Auth;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly IUserService _userService;

    public CustomAuthStateProvider(IUserService userService)
    {
        _userService = userService;
    }
    
    private ClaimsPrincipal _anonymous = new(new ClaimsIdentity());
    
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        ClaimsPrincipal? user = await GetUserFromServer();
        
        return new AuthenticationState(user ?? _anonymous);
    }

    public void NotifyUserLoggedOut()
    {
        var authState = Task.FromResult(new AuthenticationState(_anonymous));
        NotifyAuthenticationStateChanged(authState);
    }

    public async Task<bool> NotifyUserLoggedIn()
    {
        ClaimsPrincipal? user = await GetUserFromServer();
        if (user is null)
        {
            return false;
        }
        
        var authState = Task.FromResult(new AuthenticationState(user));
        NotifyAuthenticationStateChanged(authState);
        return true;
    }
    
    private async Task<ClaimsPrincipal?> GetUserFromServer()
    {
        CurrentUserResponse? response = await _userService.GetCurrentUser();
        if (response is null)
            return null;
        
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, response.UserId),
            new Claim(ClaimTypes.Name, response.UserName)
        };

        var identity = new ClaimsIdentity(claims, "am-cookies");
        return new ClaimsPrincipal(identity);
    }
}