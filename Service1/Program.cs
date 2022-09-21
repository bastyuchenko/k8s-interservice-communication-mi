using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Service1;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .Configure<AzureAd>(builder.Configuration.GetSection("AzureAd"))
    .Configure<HttpClientSettings>(builder.Configuration.GetSection("HttpClientSettings"))
    .AddTransient<IAccessTokenProvider, AzureAdAccessTokenProvider>();

builder.Services
    .AddHttpClient("GitHub", httpClient => { httpClient.BaseAddress = new Uri("https://localhost:7019/"); })
    .AddHttpMessageHandler(AuthenticationHandler.AuthenticationHandlerFactory);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();