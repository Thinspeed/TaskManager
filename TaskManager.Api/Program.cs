using AppDefinition.Extensions;
using Microsoft.AspNetCore.CookiePolicy;
using Serilog;
using TaskManager.Api.AppDefinitions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.AddAppDefinitions();

var app = builder.Build();

try {
    app.InitAppDefinitions();
    
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseExceptionHandler();

    app.UseCors(CorsDefinition.CorsPolicyName);
    app.UseCookiePolicy(new CookiePolicyOptions()
    {
        MinimumSameSitePolicy = SameSiteMode.None,
        Secure = CookieSecurePolicy.Always,
        HttpOnly = HttpOnlyPolicy.Always
    });

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseHttpsRedirection();

    app.Run();
}
finally
{
    Log.CloseAndFlush();
}
