using AppDefinition.Abstractions;
using JetBrains.Annotations;
using Serilog;

namespace TaskManager.Api.AppDefinitions;

[UsedImplicitly]
public class SerilogDefinition : IAppDefinition
{
    public void RegisterDefinition(IHostApplicationBuilder builder)
    {
        builder.Services.AddLogging(builder =>
        {
            builder.ClearProviders();
            builder.AddSerilog();
        });

        builder.Services.AddSerilog(loggerConfiguration =>
        {
            loggerConfiguration.ReadFrom.Configuration(builder.Configuration);
            
            Serilog.Debugging.SelfLog.Enable(Console.Error);
        });
    }
}