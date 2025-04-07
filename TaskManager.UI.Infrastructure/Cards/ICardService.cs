using TaskManager.UI.Infrastructure.Cards.Contracts;
using TaskManager.UI.Infrastructure.Shared;

namespace TaskManager.UI.Infrastructure.Cards;

public interface ICardService
{
    Task<int> CreateAsync(CreateCardRequest request);
    Task<bool> StartAsync(int id);
    Task<bool> CompleteAsync(int id);
    Task<PagedList<GetCardResponse>?> GetAsync(string sorts, string filters, int page, int pageSize);
    Task<GetCardResponse?> GetByIdAsync(int id);
    Task<bool> DeleteAsync(int id);
}