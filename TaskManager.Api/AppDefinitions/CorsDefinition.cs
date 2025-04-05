using AppDefinition.Abstractions;

namespace TaskManager.Api.AppDefinitions;

public class CorsDefinition : IAppDefinition
{
    public const string CorsPolicyName = "corsDefaultPolicy";
    
    public void RegisterDefinition(IHostApplicationBuilder builder)
    {
        string allowedHosts = builder.Configuration.GetSection("CorsOrigins").Value ?? string.Empty;
        string[] origins = allowedHosts.Split(" ");
        
        builder.Services.AddCors(options => options.AddPolicy(name: CorsPolicyName, policy =>
        {
            policy.WithOrigins(origins)
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        }));
    }

    public void Init(IHost app)
    {
        if (app is not IApplicationBuilder appBuilder)
        {
            throw new Exception("App definitions must implement IApplicationBuilder interface to add Cors middleware.");
        }
        
        
    }
}