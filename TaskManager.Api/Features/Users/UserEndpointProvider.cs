using JetBrains.Annotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Features.Users.Queries;

namespace TaskManager.Api.Features.Users;

[UsedImplicitly]
public class UserEndpointProvider : IEndpointProvider
{
    public void RegisterEndpoints(IEndpointRouteBuilder builder)
    {
        builder.MapPost(
            "/register", 
            async ([FromServices] IMediator mediator, [AsParameters] CreateUserCommand request) => Results.Ok(await mediator.Send(request)));

        builder.MapPost(
            "/login",
            async ([FromServices] IMediator mediator, [AsParameters] LoginCommand request) => Results.Ok(await mediator.Send(request)));
        
        builder.MapGet(
            "/user/{Id}",
            async (IMediator mediator, [AsParameters] GetUserByIdQuery request) => Results.Ok(await mediator.Send(request)));
    }
}