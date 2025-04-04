using System.Text;
using AppDefinition.Abstractions;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
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

        JwtOptions jwtOptions = builder.Configuration.GetSection("JwtOptions").Get<JwtOptions>()
            ?? throw new Exception("JwtOptions not found");

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["am-cookies"];

                        return Task.CompletedTask;
                    },
                    // OnAuthenticationFailed = context =>
                    // {
                    //     Console.WriteLine($"JWT ERROR: {context.Exception.Message}");
                    //     return Task.CompletedTask;
                    // }
                };
            });

        builder.Services.AddAuthorization();
    }

    public void Init(IHost app)
    {
        if (app is not IApplicationBuilder appBuilder)
        {
            throw new Exception($"App doest not implement {nameof(IApplicationBuilder)}");
        }
        
        appBuilder.UseAuthentication();
        appBuilder.UseAuthorization();
    }
}