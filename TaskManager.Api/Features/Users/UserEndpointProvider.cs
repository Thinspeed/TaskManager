using JetBrains.Annotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Features.Users.Queries;
using TaskManager.Infrastructure.JwtProvider;

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
                (HttpContext context) =>
                {
                    context.Response.Cookies.Delete("am-cookies");

                    return Results.Ok();
                })
            .RequireAuthorization();
        
        builder.MapGet(
                "/user/status",
                ([FromServices] IJwtProvider jwtProvider, HttpContext context) =>
                {
                    var cookie = context.Request.Cookies["am-cookies"];

                    return Results.Ok(jwtProvider.GetUserInfo(cookie!));
                })
            .RequireAuthorization();
        
        builder.MapGet(
                "/user/{Id}",
                async (IMediator mediator, [AsParameters] GetUserByIdQuery request) => Results.Ok(await mediator.Send(request)))
            .RequireAuthorization();
    }
}