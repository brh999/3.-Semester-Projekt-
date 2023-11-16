using WebApi.BuissnessLogiclayer;
using WebApi.Controllers;
using WebApi.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddSingleton<IAccountLogic, AccountLogic>();
builder.Services.AddSingleton<IAccountDBAccess, AccountDBAccess>();
builder.Services.AddSingleton<IPostDBAccess, PostDBAccess>();
builder.Services.AddSingleton<IPostLogic, PostLogic>();

var app = builder.Build();



// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
