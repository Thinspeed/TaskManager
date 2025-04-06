using TaskManager.UI.Extensions;
using TaskManager.UI.Infrastructure.Comments;
using TaskManager.UI.Infrastructure.Comments.Contracts;
using TaskManager.UI.Infrastructure.Shared;
using TaskManager.UI.Services.Common;

namespace TaskManager.UI.Services.Comments;

public class CommentService : ICommentService
{
    private readonly AuthorizedHttpClient _client;
    
    public CommentService(AuthorizedHttpClient client)
    {
        _client = client;
    }
    
    public async Task<int> CreateAsync(CreateCommentRequest request)
    {
        HttpResponseMessage response = await _client.PostJsonAsync("comment/", request);

        return !response.IsSuccessStatusCode ? -1 : int.Parse(await response.Content.ReadAsStringAsync());
    }

    public async Task<PagedList<GetCommentResponse>?> GetAsync(string sorts, string filters, int page, int pageSize)
    {
        string uri = "/comment".CreateSieveUri(sorts, filters, page, pageSize);

        PagedList<GetCommentResponse>? response = await _client.GetJsonAsync<PagedList<GetCommentResponse>>(uri);
        
        return response;
    }
}