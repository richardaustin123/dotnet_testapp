using GameStore.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);
// configuration of request pipeline
var app = builder.Build();

List<GameDto> games = [
    new GameDto(1, "The Last of Us Part II", "Action", 12m, new DateOnly(2020, 6, 19)),
    new GameDto(2, "Outer Wilds", "Adventure", 1.2m, new DateOnly(2019, 5, 30)),
    new GameDto(3, "Cyberpunk 2077", "RPG", 18m, new DateOnly(2020, 12, 10)),
    new GameDto(4, "Vampire Survivors", "Indie", 4m, new DateOnly(2021, 12, 17)),
    new GameDto(5, "Spider-Man", "Action", 25m, new DateOnly(2018, 9, 7))
];

app.MapGet("games", () => games);

// app.MapGet("/", () => "Hello World!");
app.MapGet("/", () => "Hello World!");

app.Run();