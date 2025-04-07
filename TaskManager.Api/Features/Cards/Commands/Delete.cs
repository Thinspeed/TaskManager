using EntityFramework.Persistence;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Exceptions;
using TaskManager.Api.Requests;
using TaskManager.Domain;

namespace TaskManager.Api.Features.Cards;

[UsedImplicitly]
public class DeleteCardCommand : DeleteCommand<int, Unit>
{
    public int UserId { get; set; }
}

[UsedImplicitly]
public class DeleteCardCommandHandler(ApplicationDbContext dbContext) : BaseRequestHandler<DeleteCardCommand, Unit>
{
    public override async Task<Unit> Handle(DeleteCardCommand request, CancellationToken cancellationToken)
    {
        Card card = await dbContext.Set<Card>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
                    ?? throw new NotFoundException("Задача не найдена");

        if (card.UserId != request.UserId)
        {
            throw new AuthException("Отказано в доступе", "Пользователь не является владельцем задачи");
        }

        dbContext.Set<Card>().Remove(card);

        await dbContext.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}