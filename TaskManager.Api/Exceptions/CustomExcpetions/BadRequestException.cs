namespace TaskManager.Api.Exceptions;

public class BadRequestException(string message) : BackendException(message)
{
    public override int StatusCode => 400;
}