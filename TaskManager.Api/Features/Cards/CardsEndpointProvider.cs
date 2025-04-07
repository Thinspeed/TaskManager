using System.Security.Claims;
using JetBrains.Annotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;
using TaskManager.Api.Extensions;

namespace TaskManager.Api.Features.Cards;

[UsedImplicitly]
public class CardsEndpointProvider : IEndpointProvider
{
    public void RegisterEndpoints(IEndpointRouteBuilder builder)
    {
        builder.MapPost(
                "/card",
                async ([FromServices] IMediator mediator, [FromBody] CreateCardCommandBody requestBody, ClaimsPrincipal user) =>
                {
                    var request = new CreateCardCommand()
                    {
                        Body = requestBody,
                        UserId = user.GetUserId()
                    };
                    
                    return Results.Ok(await mediator.Send(request));
                })
            .RequireAuthorization();

        builder.MapPut(
                "/card/{Id}/start",
                async ([FromServices] IMediator mediator, [FromRoute] int id,
                    ClaimsPrincipal user) =>
                {
                    var request = new StartProcessingCardCommand()
                    {
                        CardId = id,
                        UserId = user.GetUserId()
                    };
                    
                    return Results.Ok(await mediator.Send(request));
                })
            .RequireAuthorization();

        builder.MapPut(
                "/card/{Id}/complete",
                async ([FromServices] IMediator mediator, [FromRoute] int id,
                    ClaimsPrincipal user) =>
                {
                    var request = new CompleteCardCommand()
                    {
                        CardId = id,
                        UserId = user.GetUserId()
                    };

                    return Results.Ok(await mediator.Send(request));
                })
            .RequireAuthorization();
        
        builder.MapDelete(
            "/card/{Id}",
            async ([FromServices] IMediator mediator, [FromRoute] int id,
                ClaimsPrincipal user) =>
            {
                var request = new DeleteCardCommand()
                {
                    Id = id,
                    UserId = user.GetUserId()
                };

                return Results.Ok(await mediator.Send(request));
            })
            .RequireAuthorization();

        builder.MapGet(
                "/card",
                async ([FromServices] IMediator mediator, [AsParameters] SieveModel sieveModel) =>
                {
                    var request = new GetCardQuery()
                    {
                        SieveModel = sieveModel
                    };

                    return Results.Ok(await mediator.Send(request));
                })
            .RequireAuthorization();
        
        builder.MapGet(
                "/card/{Id}",
                async ([FromServices] IMediator mediator, [AsParameters] GetCardByIdQuery request) => 
                Results.Ok(await mediator.Send(request)))
            .RequireAuthorization();
    }
}