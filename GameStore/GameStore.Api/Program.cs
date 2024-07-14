using GameStore.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);
// configuration of request pipeline
var app = builder.Build();

const string GetGameEndpointName = "GetGame";

List<GameDto> games = [
    new GameDto(1, "The Last of Us Part II", "Action", 12m, new DateOnly(2020, 6, 19)),
    new GameDto(2, "Outer Wilds", "Adventure", 1.2m, new DateOnly(2019, 5, 30)),
    new GameDto(3, "Cyberpunk 2077", "RPG", 18m, new DateOnly(2020, 12, 10)),
    new GameDto(4, "Vampire Survivors", "Indie", 4m, new DateOnly(2021, 12, 17)),
    new GameDto(5, "Spider-Man", "Action", 25m, new DateOnly(2018, 9, 7))
];

// app.MapGet("/", () => "Hello World!");

// GET /games
app.MapGet("games", () => games);

// GET /games/1
app.MapGet("games/{id}", (int id) => games.Find(game => game.Id == id))
    .WithName(GetGameEndpointName);

// POST /games
app.MapPost("games", (CreateGameDto newGame) => {
    GameDto game = new (
        games.Count + 1, 
        newGame.Name, 
        newGame.Genre, 
        newGame.Price, 
        newGame.ReleaseDate
    );
    
    games.Add(game);
    
    return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
});

// PUT /games
app.MapPut("games/{id}", (int id, UpdateGameDto updatedGame) => {
    
    var index = games.FindIndex(game => game.Id == id);

    games[index] = new GameDto(
        id,
        updatedGame.Name,
        updatedGame.Genre,
        updatedGame.Price,
        updatedGame.ReleaseDate
    );

    return Results.NoContent();

    // GameDto game = games.Find(game => game.Id == id);
    
    // if (game is null)
    // {
    //     return Results.NotFound();
    // }
    
    // games.Remove(game);
    
    // game = game with {
    //     Name = updatedGame.Name,
    //     Genre = updatedGame.Genre,
    //     Price = updatedGame.Price,
    //     ReleaseDate = updatedGame.ReleaseDate
    // };
    
    // games.Add(game);
    
    // return Results.NoContent();
});

// DELETE /games/1
app.MapDelete("games/{id}", (int id) => {
    games.RemoveAll(game => game.Id == id);

    return Results.NoContent();
});

app.Run();
