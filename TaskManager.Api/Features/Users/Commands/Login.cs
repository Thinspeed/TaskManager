using EntityFramework.Persistence;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Exceptions;
using TaskManager.Api.Requests;
using TaskManager.Domain;
using TaskManager.Infrastructure.JwtProvider;

namespace TaskManager.Api.Features.Users;

[UsedImplicitly]
public record LoginCommandBody(
    string Login,
    string Password) : PostCommandBody;

[UsedImplicitly]
public class LoginCommand : PostCommand<LoginCommandBody, string>;

[UsedImplicitly]
public class LoginCommandHandler(ApplicationDbContext dbContext, IPasswordHasher<User> hasher, IJwtProvider jwtProvider) 
    : BaseRequestHandler<LoginCommand, string>
{
    public override async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        LoginCommandBody body = request.Body;
        
        User user = await dbContext.Set<User>().FirstOrDefaultAsync(x => x.Login == body.Login, cancellationToken)
            ?? throw new BadRequestException("Неверный логин или пароль");

        //todo обрабатывать PasswordVerificationResult.SuccessRehashNeeded?
        if (hasher.VerifyHashedPassword(null!, user.PasswordHash, body.Password) == PasswordVerificationResult.Failed)
        {
            throw new BadRequestException("Неверный логин или пароль");
        }

        string token = jwtProvider.GenerateToken(user);

        return token;
    }
}