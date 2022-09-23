using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Service2;
using Service2.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(opt => opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Bearer", opt =>
    {
        var authenticationSettings = builder.Configuration.GetSection("Authentication").Get<AuthenticationSettings>();

        opt.Authority = $"https://login.microsoftonline.com/{authenticationSettings.TenantId}/";

        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            ValidAudience = authenticationSettings.Audience,
            ValidIssuer = $"https://login.microsoftonline.com/{authenticationSettings.TenantId}/v2.0",
        };
    });

builder.Services.AddControllers();
builder.Services.AddTransient<IAuthorizationHandler, Service2AuthHandler>();
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();