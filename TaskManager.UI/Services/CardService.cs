using TaskManager.UI.Extensions;
using TaskManager.UI.Infrastructure.Cards;
using TaskManager.UI.Infrastructure.Cards.Contracts;
using TaskManager.UI.Infrastructure.Shared;
using TaskManager.UI.Models.Cards;

namespace TaskManager.UI.Services;

public class CardService : ICardService
{
    private readonly AuthorizedHttpClient _client;
    
    public CardService(AuthorizedHttpClient client)
    {
        _client = client;
    }
    
    public async Task<int> CreateAsync(CreateCardRequest request)
    {
        request.EstimatedCompletionDate = request.EstimatedCompletionDate.ToUniversalTime();
        HttpResponseMessage response = await _client.PostJsonAsync("card/", request);

        return !response.IsSuccessStatusCode ? -1 : int.Parse(await response.Content.ReadAsStringAsync());
    }

    public async Task<bool> StartAsync(int id)
    {
        HttpResponseMessage response = await _client.PutJsonAsync("/card/start", new { CardId = id });

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> CompleteAsync(int id)
    {
        HttpResponseMessage response = await _client.PutJsonAsync("/card/complete", new { CardId = id });

        return response.IsSuccessStatusCode;
    }

    public async Task<PagedList<GetCardResponse>?> GetAsync(string sorts, string filters, int page, int pageSize)
    {
        string uri = "/card".CreateSieveUri(sorts, filters, page, pageSize);

        PagedList<GetCardResponse>? response = await _client.GetJsonAsync<PagedList<GetCardResponse>>(uri);
        
        return response;
    }

    public async Task<GetCardResponse?> GetByIdAsync(int id)
    {
        GetCardResponse? response = await _client.GetJsonAsync<GetCardResponse>($"card/{id}");
        
        return response;
    }
}