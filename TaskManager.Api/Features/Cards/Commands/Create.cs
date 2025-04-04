using System.Text.Json.Serialization;
using EntityFramework.Persistence;
using JetBrains.Annotations;
using TaskManager.Api.Requests;
using TaskManager.Domain;

namespace TaskManager.Api.Features.Cards;

[UsedImplicitly]
public record CreateCardCommandBody(
    string Name,
    string Description,
    DateTime EstimatedCompletionDate) : PostCommandBody;

[UsedImplicitly]
public class CreateCardCommand : PostCommand<CreateCardCommandBody, int>
{
    public int UserId { get; init; }
}

[UsedImplicitly]
public class CreateCardCommandHandler(ApplicationDbContext dbContext) : BaseRequestHandler<CreateCardCommand, int>
{
    public override async Task<int> Handle(CreateCardCommand request, CancellationToken cancellationToken)
    {
        CreateCardCommandBody body = request.Body;

        var card = new Card(request.UserId, body.Name, body.Description, body.EstimatedCompletionDate);
        
        await dbContext.Set<Card>().AddAsync(card, cancellationToken);
        
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return card.Id;
    }
}