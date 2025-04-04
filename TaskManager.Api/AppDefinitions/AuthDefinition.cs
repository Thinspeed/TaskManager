using AppDefinition.Abstractions;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using TaskManager.Domain;
using TaskManager.Infrastructure.JwtProvider;

namespace TaskManager.Api.AppDefinitions;

[UsedImplicitly]
public class AuthDefinition : IAppDefinition
{
    public void RegisterDefinition(IHostApplicationBuilder builder)
    {
        builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

        builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));
        builder.Services.AddScoped<IJwtProvider>(provider => 
            new JwtProvider(provider.GetRequiredService<IOptions<JwtOptions>>().Value));
    }
}