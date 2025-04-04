using System.Security.Claims;
using JetBrains.Annotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;
using TaskManager.Api.Extensions;

namespace TaskManager.Api.Features.Comments;

[UsedImplicitly]
public class CommentsEndpointProvider : IEndpointProvider
{
    public void RegisterEndpoints(IEndpointRouteBuilder builder)
    {
        builder.MapPost(
                "/comment",
                async ([FromServices] IMediator mediator, [FromBody] CreateCommentCommandBody requestBody, ClaimsPrincipal user) =>
                {
                    var request = new CreateCommentCommand()
                    {
                        Body = requestBody,
                        UserId = user.GetUserId()
                    };

                    return Results.Ok(await mediator.Send(request));
                })
            .RequireAuthorization();

        builder.MapGet(
            "/comment",
            async ([FromServices] IMediator mediator, [AsParameters] SieveModel sieveModel) =>
            {
                var request = new GetCommentQuery()
                {
                    SieveModel = sieveModel
                };

                return Results.Ok(await mediator.Send(request));
            });
    }
}