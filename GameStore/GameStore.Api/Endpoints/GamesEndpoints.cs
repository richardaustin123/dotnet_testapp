using GameStore.Api.Dtos;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints {
    
    const string GetGameEndpointName = "GetGame";

    private static readonly List<GameDto> games = [
        new GameDto(1, "The Last of Us Part II", "Action", 12m, new DateOnly(2020, 6, 19)),
        new GameDto(2, "Outer Wilds", "Adventure", 1.2m, new DateOnly(2019, 5, 30)),
        new GameDto(3, "Cyberpunk 2077", "RPG", 18m, new DateOnly(2020, 12, 10)),
        new GameDto(4, "Vampire Survivors", "Indie", 4m, new DateOnly(2021, 12, 17)),
        new GameDto(5, "Spider-Man", "Action", 25m, new DateOnly(2018, 9, 7))
    ];

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app) {

        var group = app.MapGroup("games");

        // GET /games
        group.MapGet("/", () => games);

        // GET /games/1
        group.MapGet("/{id}", (int id) => {
            GameDto? game = games.Find(game => game.Id == id);

            // if (game is null) return Results.NotFound(); else return Results.Ok(game);
            return game is null ? Results.NotFound() : Results.Ok(game);
        })
        .WithName(GetGameEndpointName);

        // POST /games
        group.MapPost("/", (CreateGameDto newGame) => {
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
        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) => {
            
            var index = games.FindIndex(game => game.Id == id);

            if (index == -1) {
                return Results.NotFound();
            }

            games[index] = new GameDto(
                id,
                updatedGame.Name,
                updatedGame.Genre,
                updatedGame.Price,
                updatedGame.ReleaseDate
            );

            return Results.NoContent();
        });

        // DELETE /games/1
        group.MapDelete("/{id}", (int id) => {
            games.RemoveAll(game => game.Id == id);
            return Results.NoContent();
        });

        return group;
    }
}
