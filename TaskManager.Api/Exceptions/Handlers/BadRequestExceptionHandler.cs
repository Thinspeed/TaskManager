using JetBrains.Annotations;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Domain.Exceptions;

namespace TaskManager.Api.Exceptions;

[UsedImplicitly]
public class BadRequestExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, 
        Exception exception, 
        CancellationToken cancellationToken)
    {
        if (exception is not DomainException && exception is not BackendException) return false;

        int statusCode = (exception as BackendException)?.StatusCode ?? StatusCodes.Status400BadRequest;
        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = "Ошибка клиента",
            Detail = exception.Message
        };

        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response
            .WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;


    }
}