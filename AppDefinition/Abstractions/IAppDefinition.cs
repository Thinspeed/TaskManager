using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AppDefinition.Abstractions;

public interface IAppDefinition
{
    public Type[] DependsOn => [];
    
    void RegisterDefinition(IHostApplicationBuilder builder);

    void Init(IHost app) { }
}