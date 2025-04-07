using EntityFramework.Persistence;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Exceptions;
using TaskManager.Api.Requests;
using TaskManager.Domain;

namespace TaskManager.Api.Features.Cards;

[UsedImplicitly]
public record CompleteCardCommandBody : PutCommandBody;

[UsedImplicitly]
public class CompleteCardCommand : PutCommandWithId<int, CompleteCardCommandBody, Unit>
{
    public int UserId { get; init; }

    public int CardId { get; init; }
}

[UsedImplicitly]
public class CompleteCardCommandHandler(ApplicationDbContext dbContext) : BaseRequestHandler<CompleteCardCommand, Unit>
{
    public override async Task<Unit> Handle(CompleteCardCommand request, CancellationToken cancellationToken)
    {
        Card card = await dbContext.Set<Card>().FirstOrDefaultAsync(x => x.Id == request.CardId, cancellationToken)
                    ?? throw new NotFoundException("Задача не найдена");

        if (card.UserId != request.UserId)
        {
            throw new AuthException("Отказано в доступе", "Пользователь не является владельцем задачи");
        }
        
        card.Complete();
        
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}