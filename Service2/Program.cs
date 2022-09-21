using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Service2;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(opt => opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Bearer", opt =>
    {
        opt.Authority = "https://login.microsoftonline.com/abastiuchenkohotmail.onmicrosoft.com/";

        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = false,
            ValidAudience = "2c3bb27b-6c9f-43bd-a589-2dfd59eadd74",
            ValidIssuer = "https://login.microsoftonline.com/88b28885-115e-4d82-b554-b8785399306e/v2.0"
        };
    });


builder.Services.AddControllers();

#region Custom

builder.Services.AddTransient<IAuthorizationHandler, Service2AuthHandler>();

builder.Services.AddAuthorization();

#endregion

var app = builder.Build();


// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();