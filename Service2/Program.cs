using Azure.Core.Pipeline;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web;
using Service2;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddControllers();

var auth = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .AddRequirements(new AuthGroupReq())
    .Build();

builder.Services.AddSingleton<IAuthorizationHandler, Service2AuthHandler>();

builder.Services.AddAuthorization(opt=>
{
    opt.DefaultPolicy = opt.FallbackPolicy = auth;
});

var app = builder.Build();



// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
