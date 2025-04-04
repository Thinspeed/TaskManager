using EntityFramework.Persistence;
using JetBrains.Annotations;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Requests;
using TaskManager.Domain;

namespace TaskManager.Api.Features.Users;

[UsedImplicitly]
public record CreateUserCommandBody(
    string Login,
    string Password,
    string Name) : PostCommandBody;

[UsedImplicitly]
public class CreateUserCommand : PostCommand<CreateUserCommandBody, Unit>;

[UsedImplicitly]
public class CreateUserCommandHandler(ApplicationDbContext dbContext, IPasswordHasher<User> hasher)
    : BaseRequestHandler<CreateUserCommand, Unit>
{
    public override async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        CreateUserCommandBody body = request.Body;

        if (await dbContext.Set<User>().AnyAsync(x => x.Login == body.Login, cancellationToken))
        {
            throw new Exception($"Пользователь с логином {body.Login} уже существует.");
        }
        
        string passwordHash = hasher.HashPassword(null!, body.Password);
        
        User user = new User(body.Login, passwordHash, body.Name);
        
        await dbContext.Set<User>().AddAsync(user, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}