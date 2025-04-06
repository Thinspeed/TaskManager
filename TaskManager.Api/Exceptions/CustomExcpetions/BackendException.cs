namespace TaskManager.Api.Exceptions;

public class BackendException(string message) : Exception(message)
{
    public virtual int StatusCode => 500;
}