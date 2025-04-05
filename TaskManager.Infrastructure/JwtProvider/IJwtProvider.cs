using TaskManager.Domain;

namespace TaskManager.Infrastructure.JwtProvider;

public interface IJwtProvider
{
    string GenerateToken(User user);

    public object GetUserInfo(string token);
}