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
            async ([FromServices] IMediator mediator, [AsParameters] LoginCommand request, HttpContext context) =>
            {
                string token = await mediator.Send(request);
                
                context.Response.Cookies.Append("am-cookies", token);

                return Results.Ok();
            });
        
        builder.MapPost(
                "/logout",
                ([FromServices] IMediator mediator, HttpContext context) =>
                {
                    context.Response.Cookies.Delete("am-cookies");

                    return Results.Ok();
                })
            .RequireAuthorization();
        
        builder.MapGet(
                "/user/{Id}",
                async (IMediator mediator, [AsParameters] GetUserByIdQuery request) => Results.Ok(await mediator.Send(request)))
            .RequireAuthorization();
    }
}