using GameStore.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);
// configuration of request pipeline
var app = builder.Build();

// app.MapGet("/", () => "Hello World!");
app.MapGamesEndpoints();

app.Run();
