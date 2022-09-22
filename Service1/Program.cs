using Microsoft.AspNetCore.Authentication.JwtBearer;
using Service1;
using Service1.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .Configure<AzureAdSettings>(builder.Configuration.GetSection("AzureADSettings"))
    .AddTransient<IAccessTokenProvider, AzureAdAccessTokenProvider>();

builder.Services
    .AddHttpClient<Service2Service>()
    .AddHttpMessageHandler(AuthenticationHandler.AuthenticationHandlerFactory);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();