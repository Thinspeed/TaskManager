using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;
using TaskManager.Api.Extensions;

namespace TaskManager.Api.Features.Cards;

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
                "/card/start",
                async ([FromServices] IMediator mediator, [FromBody] StartProcessingCardCommandBody requestBody,
                    ClaimsPrincipal user) =>
                {
                    var request = new StartProcessingCardCommand()
                    {
                        Body = requestBody,
                        UserId = user.GetUserId()
                    };
                    
                    return Results.Ok(await mediator.Send(request));
                })
            .RequireAuthorization();

        builder.MapPut(
                "/card/complete",
                async ([FromServices] IMediator mediator, [FromBody] CompleteCardCommandBody requestBody,
                    ClaimsPrincipal user) =>
                {
                    var request = new CompleteCardCommand()
                    {
                        Body = requestBody,
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