namespace TaskManager.Api.Exceptions;

public class NotFoundException(string message) : BackendException(message)
{
    public override int StatusCode => 404;
}