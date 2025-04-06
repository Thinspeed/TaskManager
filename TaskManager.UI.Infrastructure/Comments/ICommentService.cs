using TaskManager.UI.Infrastructure.Comments.Contracts;
using TaskManager.UI.Infrastructure.Shared;

namespace TaskManager.UI.Infrastructure.Comments;

public interface ICommentService
{
    Task<int> CreateAsync(CreateCommentRequest request);
    Task<PagedList<GetCommentResponse>?> GetAsync(string sorts, string filters, int page, int pageSize);
}