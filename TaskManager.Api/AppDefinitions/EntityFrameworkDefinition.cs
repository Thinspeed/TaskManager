using AppDefinition.Abstractions;
using EntityFramework.Persistence;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace TaskManager.Api.AppDefinitions;

[UsedImplicitly]
public class EntityFrameworkDefinition : IAppDefinition
{
    public void RegisterDefinition(IHostApplicationBuilder builder)
    {
        string connectionString = builder.Configuration.GetSection("ConnectionString").Value 
                                  ?? throw new InvalidOperationException("Connection string was not provided");
        
        builder.Services.AddDbContext<ApplicationDbContext>(options => options
            .UseNpgsql(connectionString));
    }
    
    public void Init(IHost app)
    {
        using IServiceScope scope = app.Services.CreateScope();
        ILogger<Program>? logger = scope.ServiceProvider.GetService<ILogger<Program>>();
        
        using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        
        logger?.LogInformation("Применение миграций");
        
        dbContext.Database.Migrate();
        
        logger?.LogInformation("Миграции применены."); 
    }
}