using AppDefinition.Abstractions;
using JetBrains.Annotations;
using TaskManager.Api.Features;

namespace TaskManager.Api.AppDefinitions;

[UsedImplicitly]
public class EndpointsDefinition : IAppDefinition
{
    public void RegisterDefinition(IHostApplicationBuilder builder)
    {
    }

    public void Init(IHost app)
    {
        if (app is not IEndpointRouteBuilder endpointRouteBuilder)
        {
            throw new Exception($"App does not implement {nameof(IEndpointRouteBuilder)}");
        }
        
        var endpointProviderTypes = typeof(EndpointsDefinition).Assembly
            .GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && typeof(IEndpointProvider).IsAssignableFrom(t));

        foreach (var providerType in endpointProviderTypes)
        {
            if (Activator.CreateInstance(providerType) is IEndpointProvider provider)
            {
                provider.RegisterEndpoints(endpointRouteBuilder);
            }
        }
    }
}