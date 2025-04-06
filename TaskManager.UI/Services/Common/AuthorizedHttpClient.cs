using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Components.WebAssembly.Http;

namespace TaskManager.UI.Services.Common;

public class AuthorizedHttpClient
{
    private readonly HttpClient _httpClient;

    public AuthorizedHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    private void AddCredentials(HttpRequestMessage request)
    {
        request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
    }

    public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
    {
        AddCredentials(request);
        return await _httpClient.SendAsync(request);
    }

    public async Task<T?> GetJsonAsync<T>(string url)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        AddCredentials(request);

        var response = await _httpClient.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        return default;
    }

    public async Task<HttpResponseMessage> PostJsonAsync<T>(string url, T data)
    {
        var json = JsonSerializer.Serialize(data);
        var request = new HttpRequestMessage(HttpMethod.Post, url)
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };
        AddCredentials(request);

        return await _httpClient.SendAsync(request);
    }

    public async Task<HttpResponseMessage> PutJsonAsync<T>(string url, T data)
    {
        var json = JsonSerializer.Serialize(data);
        var request = new HttpRequestMessage(HttpMethod.Put, url)
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };
        AddCredentials(request);

        return await _httpClient.SendAsync(request);
    }

    public async Task<HttpResponseMessage> DeleteAsync(string url)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, url);
        AddCredentials(request);

        return await _httpClient.SendAsync(request);
    }
}