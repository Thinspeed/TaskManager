using JetBrains.Annotations;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace TaskManager.Api.Exceptions;

[UsedImplicitly]
public class AuthExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, 
        Exception exception, 
        CancellationToken cancellationToken)
    {
        if (exception is not AuthException authException) return false;
        
        var problemDetails = new ProblemDetails
        {
            Status = authException.StatusCode,
            Title = authException.Title,
            Detail = exception.Message
        };

        httpContext.Response.StatusCode = authException.StatusCode;
        
        await httpContext.Response
            .WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;


    }
}