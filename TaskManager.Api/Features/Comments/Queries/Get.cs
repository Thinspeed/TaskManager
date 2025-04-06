using EFSelector;
using EntityFramework.Persistence;
using JetBrains.Annotations;
using Sieve.Services;
using TaskManager.Api.Models;
using TaskManager.Api.Requests;
using TaskManager.Domain;

namespace TaskManager.Api.Features.Comments;

[UsedImplicitly]
public class GetCommentQuery : GetQuery<GetCommentResponse>;

[UsedImplicitly]
public class GetCommentByCardIdQueryHandler(ApplicationDbContext dbContext, SieveProcessor sieveProcessor)
    : BaseRequestHandler<GetCommentQuery, GetCommentResponse, PagedList<GetCommentResponse>>(dbContext,
        sieveProcessor)
{
    private static readonly Selector<Comment, GetCommentResponse> Selector =
        GetCommentResponse.Selector.Construct();
    
    public override async Task<PagedList<GetCommentResponse>> Handle(GetCommentQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Comment> query = DbContext.Set<Comment>();
        
        return await CreatePagedListAsync(
            query, 
            request.SieveModel,
            Selector.Expression,
            cancellationToken);
    }
}

[UsedImplicitly]
public class GetCommentResponse
{
    public int Id { get; set; }
    
    public string Content { get; set; }
    
    public DateTime CreationDate { get; set; }

    public UserModel User { get; set; }

    public static readonly EfSelector<Comment, GetCommentResponse> Selector =
        EfSelector.Declare<Comment, GetCommentResponse>()
            .Select(src => src.Id, dst => dst.Id)
            .Select(src => src.Content, dst => dst.Content)
            .Select(src => src.CreationDate, dst => dst.CreationDate)
            .Select(src => src.User, dst => dst.User, UserModel.Selector);
}