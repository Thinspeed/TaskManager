namespace TaskManager.Api.Features;

public interface IEndpointProvider
{
    void RegisterEndpoints(IEndpointRouteBuilder builder);
}