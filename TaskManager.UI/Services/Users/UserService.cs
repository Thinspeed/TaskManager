using TaskManager.UI.Infrastructure.Shared.Contracts;
using TaskManager.UI.Infrastructure.User;
using TaskManager.UI.Services.Common;

namespace TaskManager.UI.Services.Users;

public class UserService : IUserService
{
    private readonly AuthorizedHttpClient _client;
    
    public UserService(AuthorizedHttpClient client)
    {
        _client = client;
    }
    
    public async Task<int> Register(RegisterRequest request)
    {
        HttpResponseMessage response = await _client.PostJsonAsync("/register", request);

        return !response.IsSuccessStatusCode ? -1 : int.Parse(await response.Content.ReadAsStringAsync());
    }

    public async Task<bool> Login(LoginRequest request)
    {
        HttpResponseMessage response = await _client.PostJsonAsync("/login", request);
        
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> Logout()
    {
        HttpResponseMessage response = await _client.PostJsonAsync("/logout", new { });
        
        return response.IsSuccessStatusCode;
    }

    public async Task<CurrentUserResponse?> GetCurrentUser()
    {
        CurrentUserResponse? response = await _client.GetJsonAsync<CurrentUserResponse>("/user/status");

        return response;
    }

    public async Task<GetUserResponse?> GetUserById(int id)
    {
        GetUserResponse? response = await _client.GetJsonAsync<GetUserResponse>($"/user/{id}");

        return response;
    }
}