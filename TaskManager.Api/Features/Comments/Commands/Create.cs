using EntityFramework.Persistence;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TaskManager.Api.Requests;
using TaskManager.Domain;

namespace TaskManager.Api.Features.Comments;

[UsedImplicitly]
public record CreateCommentCommandBody(
    int CardId,
    string Content) : PostCommandBody;

[UsedImplicitly]
public class CreateCommentCommand : PostCommand<CreateCommentCommandBody, int>
{
    public int UserId { get; init; }
}

[UsedImplicitly]
public class CreateCommentCommandHandler(ApplicationDbContext dbContext) : BaseRequestHandler<CreateCommentCommand, int>
{
    public override async Task<int> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var entity = new Comment(request.UserId, request.Body.CardId, request.Body.Content);

        await dbContext.Set<Comment>().AddAsync(entity, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}