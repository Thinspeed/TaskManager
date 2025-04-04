using EFSelector;
using EntityFramework.Persistence;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Models;
using TaskManager.Api.Requests;
using TaskManager.Domain;

namespace TaskManager.Api.Features.Cards;

[UsedImplicitly]
public class GetCardByIdQuery : GetByIdQuery<int, GetCardByIdQueryResponse>;

[UsedImplicitly]
public class GetCardByIdQueryHandler(ApplicationDbContext dbContext)
    : BaseRequestHandler<GetCardByIdQuery, GetCardByIdQueryResponse>
{
    private static readonly Selector<Card, GetCardByIdQueryResponse> Selector =
        GetCardByIdQueryResponse.Selector.Construct();
    
    public override async Task<GetCardByIdQueryResponse> Handle(GetCardByIdQuery request, CancellationToken cancellationToken)
    {
        GetCardByIdQueryResponse entity = await dbContext.Set<Card>()
            .Select(Selector.Expression)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
            ?? throw new Exception("Задача не найдена");

        return entity;
    }
}

[UsedImplicitly]
public class GetCardByIdQueryResponse
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public DateTime CreationDate { get; set; }
    
    public DateTime? ClosingDate { get; set; }
    
    public long? ActualProcessingTime { get; set; }
    
    public DateTime EstimatedCompletionDate { get; set; }
    
    public Status Status { get; set; }

    public UserModel User { get; set; }

    public static readonly EfSelector<Card, GetCardByIdQueryResponse> Selector =
        EfSelector.Declare<Card, GetCardByIdQueryResponse>()
            .Select(src => src.Id, dst => dst.Id)
            .Select(src => src.Name, dst => dst.Name)
            .Select(src => src.Description, dst => dst.Description)
            .Select(src => src.CreationDate, dst => dst.CreationDate)
            .Select(src => src.ClosingDate, dst => dst.ClosingDate)
            .Select(src => src.ActualProcessingTime, dst => dst.ActualProcessingTime)
            .Select(src => src.EstimatedCompletionDate, dst => dst.EstimatedCompletionDate)
            .Select(src => src.Status, dst => dst.Status)
            .Select(src => src.User, dst => dst.User, UserModel.Selector);
}