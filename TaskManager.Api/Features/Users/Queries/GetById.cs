using EFSelector;
using EntityFramework.Persistence;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Requests;
using TaskManager.Domain;

namespace TaskManager.Api.Features.Users.Queries;

[UsedImplicitly]
public class GetUserByIdQuery : GetByIdQuery<int, GetUserByIdQueryResponse>;

[UsedImplicitly]
public class GetUserByIdQueryHandler(ApplicationDbContext dbContext)
    : BaseRequestHandler<GetUserByIdQuery, GetUserByIdQueryResponse>
{
    private static readonly Selector<User, GetUserByIdQueryResponse> Selector =
        GetUserByIdQueryResponse.Selector.Construct();
    
    public override async Task<GetUserByIdQueryResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        GetUserByIdQueryResponse entity =  await 
            dbContext.Set<User>().Select(Selector.Expression).FirstOrDefaultAsync(cancellationToken)
            ?? throw new Exception("Пользователь не найден");
        
        return entity;
    }
} 

[UsedImplicitly]
public class GetUserByIdQueryResponse
{
    public int Id { get; init; }
    
    public string Name { get; init; }

    public static readonly EfSelector<User, GetUserByIdQueryResponse> Selector =
        EfSelector.Declare<User, GetUserByIdQueryResponse>()
            .Select(src => src.Id, dst => dst.Id)
            .Select(src => src.Name, dst => dst.Name);
}