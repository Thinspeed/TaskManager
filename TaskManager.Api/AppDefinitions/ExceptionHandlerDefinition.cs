using AppDefinition.Abstractions;
using JetBrains.Annotations;
using TaskManager.Api.Exceptions;

namespace TaskManager.Api.AppDefinitions;

[UsedImplicitly]
public class ExceptionHandlerDefinition : IAppDefinition
{
    public void RegisterDefinition(IHostApplicationBuilder builder)
    {
        builder.Services.AddExceptionHandler<AuthExceptionHandler>();
        builder.Services.AddExceptionHandler<BadRequestExceptionHandler>();
        builder.Services.AddProblemDetails();
    }
}