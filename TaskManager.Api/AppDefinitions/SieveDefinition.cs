using AppDefinition.Abstractions;
using JetBrains.Annotations;
using Sieve.Models;
using Sieve.Services;
using TaskManager.Api.Sieve;

namespace TaskManager.Api.AppDefinitions;

[UsedImplicitly]
public class SieveDefinition : IAppDefinition
{
    public void RegisterDefinition(IHostApplicationBuilder builder)
    {
        builder.Services.Configure<SieveOptions>(builder.Configuration.GetSection("Sieve"));

        builder.Services.AddScoped<SieveProcessor, AppSieveProcessor>();
    }
}