using Microsoft.IdentityModel.Tokens;
using WebApi.BuissnessLogiclayer;
using WebApi.Database;
using WebApi.Security;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddSingleton<IAccountLogic, AccountLogic>();
builder.Services.AddSingleton<IAccountDBAccess, AccountDBAccess>();
builder.Services.AddSingleton<IPostDBAccess, PostDBAccess>();
builder.Services.AddSingleton<IPostLogic, PostLogic>();
builder.Services.AddSingleton<ICurrencyLogic, CurrencyLogic>();
builder.Services.AddSingleton<ICurrencyDBAccess, CurrencyDBAccess>();
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

// Configure the JWT Authentication Service
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
            ValidIssuer = "http://localhost:5042",
            ValidAudience = "http://localhost:5042",
            ValidateLifetime = true
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
