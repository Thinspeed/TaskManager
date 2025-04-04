using EFSelector;
using EntityFramework.Persistence;
using JetBrains.Annotations;
using Sieve.Services;
using TaskManager.Api.Requests;
using TaskManager.Domain;

namespace TaskManager.Api.Features.Cards;

[UsedImplicitly]
public class GetCardQuery : GetQuery<GetCardByIdQueryResponse>;

[UsedImplicitly]
public class GetCardQueryHandler(ApplicationDbContext dbContext, SieveProcessor sieveProcessor)
    : BaseRequestHandler<GetCardQuery, GetCardByIdQueryResponse, PagedList<GetCardByIdQueryResponse>>(dbContext,
        sieveProcessor)
{
    private static readonly Selector<Card, GetCardByIdQueryResponse> Selector =
        GetCardByIdQueryResponse.Selector.Construct();
    
    public override async Task<PagedList<GetCardByIdQueryResponse>> Handle(GetCardQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Card> query = DbContext.Set<Card>();
        
        return await CreatePagedListAsync(
            query, 
            request.SieveModel,
            Selector.Expression,
            cancellationToken);
    }
}
