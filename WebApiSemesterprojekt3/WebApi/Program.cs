using Microsoft.IdentityModel.Tokens;
using WebApi.BuissnessLogiclayer;
using WebApi.Controllers;
using WebApi.Database;
using WebApi.Security;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddSingleton<IAccountLogic, AccountLogic>();
builder.Services.AddSingleton<IAccountDBAccess, AccountDBAccess>();
builder.Services.AddSingleton<IPostDBAccess, PostDBAccess>();
builder.Services.AddSingleton<IPostLogic, PostLogic>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "JwtBearer";
    options.DefaultChallengeScheme = "JwtBearer";
})
    .AddJwtBearer("JwtBearer", jwtOptions =>
    {
        jwtOptions.TokenValidationParameters = new TokenValidationParameters()
        {
            // The SigningKey is defined in the TokenController class
            ValidateIssuerSigningKey = true,
            // IssuerSigningKey = new SecurityHelper(configuration).GetSecurityKey(),
            IssuerSigningKey = new SecurityHelper(builder.Configuration).GetSecurityKey(),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = "https://localhost:5042/",
            ValidAudience = "https://localhost:5042/",
            ValidateLifetime = true
        };
    }).AddJwtBearer();

var app = builder.Build();



// Configure the HTTP request pipeline.

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
