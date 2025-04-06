namespace TaskManager.Api.Exceptions;

public class AuthException(string title, string message) : BackendException(message)
{
    public string Title { get; init; } = title;

    public override int StatusCode => 401;
}