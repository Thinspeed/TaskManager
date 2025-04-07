using Blazored.Toast.Services;
using TaskManager.UI.Extensions;
using TaskManager.UI.Infrastructure.Shared.Contracts;
using TaskManager.UI.Infrastructure.User;
using TaskManager.UI.Services.Common;

namespace TaskManager.UI.Services.Users;

public class UserService : IUserService
{
    private readonly AuthorizedHttpClient _client;
    private readonly IToastService _toastService;
    
    public UserService(AuthorizedHttpClient client, IToastService toastService )
    {
        _client = client;
        _toastService = toastService;
    }
    
    public async Task<int> Register(RegisterRequest request)
    {
        HttpResponseMessage response = await _client.PostJsonAsync("register", request);
        await response.HandleErrors(_toastService);

        return !response.IsSuccessStatusCode ? -1 : int.Parse(await response.Content.ReadAsStringAsync());
    }

    public async Task<bool> Login(LoginRequest request)
    {
        HttpResponseMessage response = await _client.PostJsonAsync("login", request);
        await response.HandleErrors(_toastService);
        
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> Logout()
    {
        HttpResponseMessage response = await _client.PostJsonAsync("logout", new { });
        await response.HandleErrors(_toastService);
        
        return response.IsSuccessStatusCode;
    }

    public async Task<CurrentUserResponse?> GetCurrentUser()
    {
        CurrentUserResponse? response = await _client.GetJsonAsync<CurrentUserResponse>("user/status");

        return response;
    }

    public async Task<GetUserResponse?> GetUserById(int id)
    {
        GetUserResponse? response = await _client.GetJsonAsync<GetUserResponse>($"user/{id}");

        return response;
    }
}