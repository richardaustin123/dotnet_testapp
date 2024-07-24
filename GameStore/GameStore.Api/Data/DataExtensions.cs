using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public static class DataExtensions {
    public static void MigrateDb(this WebApplication app) {
        using var scope = app.Services.CreateScope(); // Create a new scope to retrieve scoped services
        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
        dbContext.Database.Migrate();
    }
}
