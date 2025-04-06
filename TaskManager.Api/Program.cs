using AppDefinition.Extensions;
using Microsoft.AspNetCore.CookiePolicy;
using TaskManager.Api.AppDefinitions;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8080);
    options.ListenAnyIP(8081, listenOptions =>
    {
        listenOptions.UseHttps("/https/cert.pfx", "Password");
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.AddAppDefinitions();

var app = builder.Build();

app.InitAppDefinitions();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCookiePolicy(new CookiePolicyOptions()
{
    MinimumSameSitePolicy = SameSiteMode.None,
    Secure = CookieSecurePolicy.Always,
    HttpOnly = HttpOnlyPolicy.Always
});

app.UseHttpsRedirection();

app.Run();