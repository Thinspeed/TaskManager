using System.Net;
using System.Text.Json;
using Blazored.Toast.Services;

namespace TaskManager.UI.Extensions;

public static class HttpExtensions
{
    public static async Task HandleErrors(this HttpResponseMessage response, IToastService toastService)
    {
        if (response.IsSuccessStatusCode)
        {
            return;
        }
        
        string content = await response.Content.ReadAsStringAsync();
        ProblemDetails? details = JsonSerializer.Deserialize<ProblemDetails>(content, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        });
        
        toastService.ShowError(details?.Detail ?? content);
    }

    private class ProblemDetails
    {
        public string Title { get; set; }
        
        public string Detail { get; set; }
        
        public int Status { get; set; }
    }
}