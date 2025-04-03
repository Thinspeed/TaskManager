namespace TaskManager.Infrastructure.JwtProvider;

public class JwtOptions
{
    public required string SecretKey { get; init; }
    
    public int ExpireInHours { get; init; }
}